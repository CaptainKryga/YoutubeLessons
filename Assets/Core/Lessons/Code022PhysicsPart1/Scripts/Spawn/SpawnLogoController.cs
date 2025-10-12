using UnityEngine;

namespace Lessons.Code022PhysicsPart1.Scripts.Spawn
{
    public class SpawnLogoController : SpawnDelayControllerBase
    {
        [SerializeField] private float _force;
        
        protected override void InstantiatePrefab()
        {
            Rigidbody rb = Instantiate(PrefabBall, Content.position, Quaternion.identity, Content);
            rb.AddForce(Content.forward * _force);
        }
    }
}
