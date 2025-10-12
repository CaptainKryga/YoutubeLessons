using UnityEngine;

namespace Lessons.Code022PhysicsPart1.Scripts.Equals
{
	public class EqualsSleepThreshold : EqualsControllerBase
	{
		protected override void OnDisable()
		{
			base.OnDisable();

			Debug.Log(Physics.sleepThreshold);
			Physics.sleepThreshold = 0;
		}

		protected override void OnSlider(float value)
		{
			Physics.sleepThreshold = value;
			TextInfo.text = value.ToString("F2");
		}
	}
}
