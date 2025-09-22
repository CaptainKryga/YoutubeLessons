using UnityEngine;

namespace Lessons.Code022PhysicsPart1.Scripts.Spawn
{
	public abstract class SpawnDelayControllerBase : MonoBehaviour
	{
		[SerializeField] protected Rigidbody PrefabBall;
		[SerializeField] protected Transform Content;

		[SerializeField] private float _delayDefault = 1;
		private float _delay;
		
		private void Update()
		{
			_delay -= Time.deltaTime;
			if (_delay <= 0)
			{
				InstantiatePrefab();
				
				_delay = _delayDefault;
			}
		}

		protected abstract void InstantiatePrefab();
	}
}