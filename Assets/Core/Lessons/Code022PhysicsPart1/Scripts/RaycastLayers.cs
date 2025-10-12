using TMPro;
using UnityEngine;

namespace Lessons.Code022PhysicsPart1.Scripts
{
    public class RaycastLayers : MonoBehaviour
    {
        [SerializeField] private Transform _ray;
        [SerializeField] private TMP_Text _info;

        [SerializeField] private bool _isAllLayers;
        [SerializeField] private bool _isDefaultLayers;
        [SerializeField] private bool _isIgnoreRaycastLayers;
        
        private void Update()
        {
            _info.text = "Мимо";
            
            if (_isAllLayers)
                if (Physics.Raycast(_ray.position, _ray.forward, 100, Physics.AllLayers))
                    _info.text = "Попали в объект через слой AllLayers!";
            
            if (_isDefaultLayers)
                if (Physics.Raycast(_ray.position, _ray.forward, 100, Physics.DefaultRaycastLayers))
                    _info.text = "Попали в объект через слой  DefaultRaycastLayers!";
            
            if (_isIgnoreRaycastLayers)
                if (Physics.Raycast(_ray.position, _ray.forward, 100, Physics.IgnoreRaycastLayer))
                    _info.text = "Попали в объект через слой  IgnoreRaycastLayer!";
            
        }
    }
}
