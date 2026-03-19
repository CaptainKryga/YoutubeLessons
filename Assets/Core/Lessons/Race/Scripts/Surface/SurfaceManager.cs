using System;
using System.Collections.Generic;
using Core.Lessons.Race.Scripts.Surface.Model;
using UnityEngine;

namespace Core.Lessons.Race.Scripts.Surface
{
    public sealed class SurfaceManager : MonoBehaviour
    {
        [Header("Database")]
        [SerializeField] private SurfacePresetDatabase database;

        [Header("WheelColliders")]
        [SerializeField] private WheelCollider[] frontWheels;
        [SerializeField] private WheelCollider[] rearWheels;

        [Header("Surface detection")]
        [SerializeField] private LayerMask groundMask = ~0;

        [Tooltip("Если true — используем SurfaceTypeTag на коллайдере под колесом. Иначе — PhysicMaterial.name.")]
        [SerializeField] private bool preferSurfaceTypeComponent = true;

        [Tooltip("Как часто переопределять поверхность (сек). 0 = каждый FixedUpdate.")]
        [Min(0f)]
        [SerializeField] private float detectInterval = 0.05f;

        [Tooltip("Какое колесо использовать как «сенсор» поверхности. Обычно rear-left или front-left.")]
        [SerializeField] private WheelCollider probeWheel;

        [Header("PhysicMaterial name mapping (fallback)")]
        [SerializeField] private List<PhysicMaterialMapping> physicMaterialMappings = new();

        [Serializable]
        public struct PhysicMaterialMapping
        {
            public string physicMaterialName;
            public SurfaceType surfaceType;
        }

        // --- Exposed multipliers for your driving logic ---
        public SurfaceType CurrentSurface { get; private set; } = SurfaceType.AsphaltDry;

        public float RearTractionMultiplier { get; private set; } = 1f;
        public float BrakeGripMultiplier { get; private set; } = 1f;
        public float SteeringGripMultiplier { get; private set; } = 1f;

        private float _timer;
        private Dictionary<string, SurfaceType> _materialNameToSurface;

        // Baselines (captured from first wheel on each axle)
        private bool _baselinesCaptured;
        private float _baseFrontWheelDampingRate;
        private float _baseRearWheelDampingRate;
        private JointSpring _baseFrontSpring;
        private JointSpring _baseRearSpring;

        private void Awake()
        {
            BuildMaterialCache();
            CaptureBaselines();
            ApplySurface(CurrentSurface, force: true);
        }

        private void FixedUpdate()
        {
            if (database == null) return;
            if (probeWheel == null)
            {
                // fallback: try use any wheel
                probeWheel = (rearWheels != null && rearWheels.Length > 0) ? rearWheels[0]
                    : (frontWheels != null && frontWheels.Length > 0) ? frontWheels[0]
                    : null;
                if (probeWheel == null) return;
            }

            if (detectInterval <= 0f)
            {
                DetectAndApply();
                return;
            }

            _timer += Time.fixedDeltaTime;
            if (_timer >= detectInterval)
            {
                _timer = 0f;
                DetectAndApply();
            }
        }

        private void DetectAndApply()
        {
            SurfaceType detected = DetectSurfaceUnderWheel(probeWheel);
            if (detected != CurrentSurface)
            {
                ApplySurface(detected, force: false);
            }
        }

        private void BuildMaterialCache()
        {
            _materialNameToSurface = new Dictionary<string, SurfaceType>(StringComparer.OrdinalIgnoreCase);
            foreach (PhysicMaterialMapping m in physicMaterialMappings)
            {
                if (string.IsNullOrWhiteSpace(m.physicMaterialName)) continue;
                _materialNameToSurface[m.physicMaterialName.Trim()] = m.surfaceType;
            }
        }

        private void CaptureBaselines()
        {
            if (_baselinesCaptured) return;

            if (frontWheels != null && frontWheels.Length > 0 && frontWheels[0] != null)
            {
                _baseFrontWheelDampingRate = frontWheels[0].wheelDampingRate;
                _baseFrontSpring = frontWheels[0].suspensionSpring;
            }

            if (rearWheels != null && rearWheels.Length > 0 && rearWheels[0] != null)
            {
                _baseRearWheelDampingRate = rearWheels[0].wheelDampingRate;
                _baseRearSpring = rearWheels[0].suspensionSpring;
            }

            _baselinesCaptured = true;
        }

        private SurfaceType DetectSurfaceUnderWheel(WheelCollider wheel)
        {
            if (wheel == null) return CurrentSurface;

            // WheelCollider.GetGroundHit быстрее/чище чем собственный Raycast во многих кейсах.
            // Но sharedMaterial берём у collider (если есть).
            if (wheel.GetGroundHit(out WheelHit hit))
            {
                Collider col = hit.collider;
                if (col == null) return CurrentSurface;

                if (preferSurfaceTypeComponent)
                {
                    if (col.TryGetComponent(out SurfaceTypeTag typeTag))
                        return typeTag.Surface;
                }

                PhysicsMaterial pm = col.sharedMaterial;
                if (pm != null && !string.IsNullOrEmpty(pm.name))
                {
                    if (_materialNameToSurface != null && _materialNameToSurface.TryGetValue(pm.name, out SurfaceType mapped))
                        return mapped;
                }
            }

            // Fallback: simple raycast from wheel position downward
            Vector3 origin = wheel.transform.position;
            Single maxDist = wheel.suspensionDistance + wheel.radius + 0.2f;

            if (Physics.Raycast(origin, -wheel.transform.up, out RaycastHit rh, maxDist, groundMask,
                    QueryTriggerInteraction.Ignore))
            {
                if (preferSurfaceTypeComponent)
                {
                    if (rh.collider.TryGetComponent(out SurfaceTypeTag typeTag))
                        return typeTag.Surface;
                }

                PhysicsMaterial pm = rh.collider.sharedMaterial;
                if (pm != null && !string.IsNullOrEmpty(pm.name))
                {
                    if (_materialNameToSurface != null && _materialNameToSurface.TryGetValue(pm.name, out SurfaceType mapped))
                        return mapped;
                }
            }

            return CurrentSurface;
        }

        public void ApplySurface(SurfaceType type, bool force)
        {
            if (!force && type == CurrentSurface) return;
            if (database == null) return;

            if (!database.TryGetPreset(type, out WheelSurfacePreset preset) || preset == null)
            {
                // Если пресет не найден — не меняем.
                return;
            }

            CaptureBaselines();

            // Apply front
            ApplyAxle(frontWheels, preset.front, isFront: true);

            // Apply rear
            ApplyAxle(rearWheels, preset.rear, isFront: false);

            // Publish multipliers for your movement logic
            RearTractionMultiplier = Mathf.Max(0f, preset.rear.tractionMultiplier);
            BrakeGripMultiplier = Mathf.Max(0f, Mathf.Min(preset.front.brakeGripMultiplier, preset.rear.brakeGripMultiplier));
            SteeringGripMultiplier = Mathf.Max(0f, preset.front.steeringGripMultiplier);

            CurrentSurface = type;
        }

        private void ApplyAxle(WheelCollider[] wheels, AxleSurfaceSettings settings, bool isFront)
        {
            if (wheels == null) return;

            foreach (WheelCollider wc in wheels)
            {
                if (wc == null) continue;

                // Friction curves
                wc.forwardFriction = settings.forwardFriction.ToCurve();
                wc.sidewaysFriction = settings.sidewaysFriction.ToCurve();

                // wheel damping
                float baseDamping = isFront ? _baseFrontWheelDampingRate : _baseRearWheelDampingRate;
                wc.wheelDampingRate = baseDamping * Mathf.Max(0.01f, settings.suspension.wheelDampingRateMultiplier);

                // suspension spring
                JointSpring baseSpring = isFront ? _baseFrontSpring : _baseRearSpring;
                JointSpring sp = wc.suspensionSpring;
                sp.spring = baseSpring.spring * Mathf.Max(0.01f, settings.suspension.springMultiplier);
                sp.damper = baseSpring.damper * Mathf.Max(0.01f, settings.suspension.damperMultiplier);

                if (settings.suspension.overrideTargetPosition)
                    sp.targetPosition = Mathf.Clamp01(settings.suspension.targetPositionOverride);
                else
                    sp.targetPosition = baseSpring.targetPosition;

                wc.suspensionSpring = sp;
            }
        }
    }
}