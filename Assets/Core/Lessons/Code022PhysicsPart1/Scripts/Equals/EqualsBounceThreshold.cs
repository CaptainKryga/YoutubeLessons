using UnityEngine;

namespace Lessons.Code022PhysicsPart1.Scripts.Equals
{
	// 0.01-4
	public class EqualsBounceThreshold : EqualsControllerBase
	{
		protected override void OnDisable()
		{
			base.OnDisable();

			Physics.bounceThreshold = 2;
		}

		protected override void OnSlider(float value)
		{
			Physics.bounceThreshold = value;
			TextInfo.text = value.ToString("F2");
		}
	}
}