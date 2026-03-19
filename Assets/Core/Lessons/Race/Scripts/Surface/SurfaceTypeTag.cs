using UnityEngine;

namespace Core.Lessons.Race.Scripts.Surface
{
    public sealed class SurfaceTypeTag : MonoBehaviour
    {
        [field: SerializeField] public SurfaceType Surface { get; private set; } = SurfaceType.AsphaltDry;
    }
}