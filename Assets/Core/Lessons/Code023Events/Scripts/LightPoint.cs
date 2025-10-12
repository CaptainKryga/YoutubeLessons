using UnityEngine;

namespace Lessons.Code023Events.Scripts
{
	[RequireComponent(typeof(Light))]
	public class LightPoint : MonoBehaviour
	{
		private Light _light;
		private ButtonSwitch _buttonSwitch;

		private void Awake()
		{
			_light = GetComponent<Light>();
			_buttonSwitch = FindAnyObjectByType<ButtonSwitch>();
		}

		private void OnEnable()
		{
			_buttonSwitch.OnButtonPressed += SwitchLight;
		}

		private void OnDisable()
		{
			_buttonSwitch.OnButtonPressed -= SwitchLight;
		}

		private void SwitchLight()
		{
			_light.enabled = !_light.enabled;
		}
	}
}
