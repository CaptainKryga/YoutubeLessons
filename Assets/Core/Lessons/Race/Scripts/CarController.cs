using UnityEngine;

namespace Core.Lessons.Race.Scripts
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;

        [SerializeField] private float _speed = 50;
        [SerializeField] private float _turnSpeed = 20;

        [SerializeField] private float _maxLinearMagnitude = 1;

        private void FixedUpdate()
        {
            float move = Input.GetAxis("Vertical");
            float turn = Input.GetAxis("Horizontal");
            
            if (_rb.linearVelocity.magnitude < _maxLinearMagnitude)
            {
                _rb.AddForce(_rb.transform.forward * move * _speed, ForceMode.Force);
            }

            if (move != 0)
            {
                turn *= move > 0 ? 1 : -1;
                _rb.transform.Rotate(Vector3.up, turn * _turnSpeed * Time.fixedDeltaTime);
            }
        }
    }
}
