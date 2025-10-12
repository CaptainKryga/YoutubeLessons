using UnityEngine;

namespace Lessons.Code022PhysicsPart1.Scripts
{
    public class ShowCode022 : MonoBehaviour
    {
        private void UpdatePhysics()
        {
            Physics.sleepThreshold = 0.005f; // Устанавливаем порог сна
        }
    }
}
