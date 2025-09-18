using System;
using UnityEngine;

namespace Lessons.Code022PhysicsPart1.Scripts
{
    public class ShowCode022 : MonoBehaviour
    {
        [Obsolete("Obsolete")]
        private void UpdatePhysics()
        {
            Physics.simulationMode = SimulationMode.Script; // Полный контроль
        }
    }
}
