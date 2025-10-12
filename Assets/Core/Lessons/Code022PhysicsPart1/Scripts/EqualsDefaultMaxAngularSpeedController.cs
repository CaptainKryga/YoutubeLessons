using Lessons.Code022PhysicsPart1.Scripts.Equals;
using UnityEngine;

namespace Lessons.Code022PhysicsPart1.Scripts
{
	//0.01-100
	public class EqualsDefaultMaxAngularSpeedController : EqualsControllerBase
	{
		[SerializeField] private Rigidbody _rb;
		[SerializeField] private float _force;

		protected override void OnDisable()
		{
			base.OnDisable();

			Physics.defaultMaxAngularSpeed = 50;
		}

		protected override void OnSlider(float value)
		{
			_rb.maxAngularVelocity = value;
			TextInfo.text = value.ToString("F2");
		}
		
		public void OnClick_Force()
		{
			_rb.angularVelocity += Vector3.one * _force;
		}
	}
}
