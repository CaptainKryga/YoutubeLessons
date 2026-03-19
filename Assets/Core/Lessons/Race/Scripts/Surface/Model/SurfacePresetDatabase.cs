using System.Collections.Generic;
using UnityEngine;

namespace Core.Lessons.Race.Scripts.Surface.Model
{
    [CreateAssetMenu(menuName = "Vehicle/Surface Preset Database", fileName = "SurfacePresetDatabase")]
    public sealed class SurfacePresetDatabase : ScriptableObject
    {
        [Tooltip("Список пресетов")]
        public List<WheelSurfacePreset> presets = new List<WheelSurfacePreset>();

        private Dictionary<SurfaceType, WheelSurfacePreset> _cache;

        public bool TryGetPreset(SurfaceType type, out WheelSurfacePreset preset)
        {
            if (_cache == null)
            {
                _cache = new Dictionary<SurfaceType, WheelSurfacePreset>();
                foreach (var p in presets)
                {
                    if (p == null) continue;
                    _cache[p.surfaceType] = p; // последний побеждает
                }
            }

            return _cache.TryGetValue(type, out preset) && preset != null;
        }
    }
}