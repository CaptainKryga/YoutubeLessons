using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Code022PhysicsPart1.Scripts.Equals
{
	public abstract class EqualsControllerBase : MonoBehaviour
	{
		[SerializeField] private Slider _slider;
		[SerializeField] protected TMP_Text TextInfo;

		protected virtual void OnEnable()
		{
			_slider.onValueChanged.AddListener(OnSlider);

			_slider.value = 0;
		}

		protected virtual void OnDisable()
		{
			_slider.value = 0;
			
			_slider.onValueChanged.RemoveListener(OnSlider);
		}

		protected abstract void OnSlider(float value);
	}
}
