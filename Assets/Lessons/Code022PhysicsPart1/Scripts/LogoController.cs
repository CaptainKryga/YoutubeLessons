using UnityEngine;

namespace Lessons.Code022PhysicsPart1.Scripts
{
    public class LogoController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _prefabBall;
        [SerializeField] private Transform _content;
        
        [SerializeField] private float _delayDefault = 1;
        private float _delay;

        [SerializeField] private float _force;
        
        private void Update()
        {
            _delay -= Time.deltaTime;
            if (_delay <= 0)
            {
                Rigidbody rb = Instantiate(_prefabBall, _content);
                rb.AddForce(_content.forward * _force);
                _delay = _delayDefault;
            }
        }
    }
}
