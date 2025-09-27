using UnityEngine;
using UnityEngine.Events;

namespace Lessons.Code023Events.Scripts
{
	public class ButtonSwitchUnityEvent : MonoBehaviour
	{
		public UnityEvent OnButtonPressed;

		public void OnClick_BtnPress()
		{
			OnButtonPressed?.Invoke();
		}
	}
}
