using UnityEngine;

namespace Lessons.Code022PhysicsPart1.Scripts.Spawn
{
	public class SpawnRandomPositionController : SpawnDelayControllerBase
	{
		[SerializeField] private float _randomValue;
		protected override void InstantiatePrefab()
		{
			Vector3 random = new Vector3(Random.Range(-_randomValue, _randomValue),
				Random.Range(-_randomValue, _randomValue), Random.Range(-_randomValue, _randomValue));
			Rigidbody rb = Instantiate(PrefabBall, Content.position + random, Quaternion.identity, Content);
		}
	}
}