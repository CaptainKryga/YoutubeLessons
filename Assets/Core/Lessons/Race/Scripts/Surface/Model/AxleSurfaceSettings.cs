using System;
using UnityEngine;

namespace Core.Lessons.Race.Scripts.Surface.Model
{
    [Serializable]
    public class AxleSurfaceSettings
    {
        public WheelFrictionPreset forwardFriction = new WheelFrictionPreset();
        public WheelFrictionPreset sidewaysFriction = new WheelFrictionPreset();

        [Header("Multipliers (used by your driving logic)")]
        [Tooltip("Для ведущей оси: масштабируйте motorTorque = baseTorque * tractionMultiplier")]
        public float tractionMultiplier = 1.0f;

        [Tooltip("Масштаб для торможения/фрикционного эффекта: brakeTorque = baseBrake * brakeGripMultiplier")]
        public float brakeGripMultiplier = 1.0f;

        [Tooltip("Масштаб для steering (или для вашей «помощи» по сцеплению при рулёжке)")]
        public float steeringGripMultiplier = 1.0f;

        [Header("Optional damping/suspension tuning")]
        public SuspensionTuning suspension = new SuspensionTuning();
    }
}