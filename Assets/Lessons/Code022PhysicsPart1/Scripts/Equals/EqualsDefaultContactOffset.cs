using UnityEngine;

namespace Lessons.Code022PhysicsPart1.Scripts.Equals
{
	// 0.01-4
	public class EqualsDefaultContactOffset : EqualsControllerBase
	{
		private void Awake()
		{
			Debug.Log(Physics.defaultContactOffset);
		}
		
		protected override void OnDisable()
		{
			base.OnDisable();

			Physics.bounceThreshold = .01f;
		}

		protected override void OnSlider(float value)
		{
			Physics.defaultContactOffset = value;
			TextInfo.text = value.ToString("F2");
		}
	}
}