using Core.Lessons.Race.Scripts.Surface;
using UnityEngine;

namespace Core.Lessons.Race.Scripts
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private SurfaceManager _surfaceManager;
        
        [SerializeField] private WheelCollider[] _frontWheels;
        [SerializeField] private WheelCollider[] _rearWheels;

        [SerializeField] private float _maxMotorForce = 1000;
        [SerializeField] private float _maxSteeringAngle = 30f;
        [SerializeField] private float _brakeForce = 1000f;

        private float _motorInput;
        private float _steeringInput;
        private float _brakeInput;
        
        private void Update()
        {
            _motorInput = Input.GetAxis("Vertical") * _maxMotorForce;
            _steeringInput = Input.GetAxis("Horizontal") * _maxSteeringAngle;
            _brakeInput = Input.GetKey(KeyCode.Space) ? _brakeForce : 0;
        }

        private void FixedUpdate()
        {
            for (int x = 0; x < _frontWheels.Length; x++)
            {
                _frontWheels[x].steerAngle = _steeringInput * _surfaceManager.SteeringGripMultiplier;
                _frontWheels[x].brakeTorque = _brakeInput * _surfaceManager.BrakeGripMultiplier;
                
                UpdateWheelPose(_frontWheels[x]);
            }
            
            for (int x = 0; x < _rearWheels.Length; x++)
            {
                _rearWheels[x].motorTorque = _motorInput * _surfaceManager.RearTractionMultiplier;
                _rearWheels[x].brakeTorque = _brakeInput * _surfaceManager.BrakeGripMultiplier;
                
                UpdateWheelPose(_rearWheels[x]);
            }
        }

        private void UpdateWheelPose(WheelCollider wheel)
        {
            if (wheel.transform.childCount == 0) return;
            
            Transform child = wheel.transform.GetChild(0);
            wheel.GetWorldPose(out Vector3 position, out Quaternion rotation);
            
            child.position = position;
            child.rotation = rotation;
        }
    }
}
