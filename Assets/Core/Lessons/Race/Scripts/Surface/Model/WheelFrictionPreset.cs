using System;
using UnityEngine;

namespace Core.Lessons.Race.Scripts.Surface.Model
{
    [Serializable]
    public class WheelFrictionPreset
    {
        public float extremumSlip = 0.4f;
        public float extremumValue = 1.0f;
        public float asymptoteSlip = 0.8f;
        public float asymptoteValue = 0.7f;
        public float stiffness = 1.0f;

        public WheelFrictionCurve ToCurve()
        {
            return new WheelFrictionCurve
            {
                extremumSlip = extremumSlip,
                extremumValue = extremumValue,
                asymptoteSlip = asymptoteSlip,
                asymptoteValue = asymptoteValue,
                stiffness = stiffness
            };
        }
    }
}
