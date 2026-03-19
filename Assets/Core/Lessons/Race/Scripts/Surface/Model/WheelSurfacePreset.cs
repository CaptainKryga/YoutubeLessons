using System;
using UnityEngine;

namespace Core.Lessons.Race.Scripts.Surface.Model
{
    [Serializable]
    public class WheelSurfacePreset
    {
        public SurfaceType surfaceType;

        [Header("Front axle (steer + brake)")]
        public AxleSurfaceSettings front = new AxleSurfaceSettings();

        [Header("Rear axle (drive + brake)")]
        public AxleSurfaceSettings rear = new AxleSurfaceSettings();
    }
}