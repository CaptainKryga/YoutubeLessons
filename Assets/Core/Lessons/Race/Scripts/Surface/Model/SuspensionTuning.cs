using System;
using UnityEngine;

namespace Core.Lessons.Race.Scripts.Surface.Model
{
    [Serializable]
    public class SuspensionTuning
    {
        [Tooltip("Множитель к wheelDampingRate")]
        public float wheelDampingRateMultiplier = 1.0f;

        [Tooltip("Множитель к suspensionSpring.spring")]
        public float springMultiplier = 1.0f;

        [Tooltip("Множитель к suspensionSpring.damper")]
        public float damperMultiplier = 1.0f;

        [Tooltip("Если true — применяем targetPositionOverride (иначе оставляем как есть).")]
        public bool overrideTargetPosition = false;

        [Range(0f, 1f)]
        public float targetPositionOverride = 0.5f;
    }
}