using UnityEngine;

namespace Lessons.Code022PhysicsPart1.Scripts.Equals
{
	public class EqualsGravity : EqualsControllerBase
	{
		protected override void OnDisable()
		{
			base.OnDisable();

			Physics.gravity = new Vector3(0, -9.81f, 0);
		}

		protected override void OnSlider(float value)
		{
			Physics.gravity = new Vector3(0, value, 0);
			TextInfo.text = value.ToString("F2");
		}
	}
}
