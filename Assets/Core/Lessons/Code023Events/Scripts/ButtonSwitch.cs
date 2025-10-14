using System;
using UnityEngine;

namespace Core.Lessons.Code023Events.Scripts
{
    public class ButtonSwitch : MonoBehaviour
    {
        public Action OnButtonPressed;

        public void OnClick_BtnPress()
        {
            OnButtonPressed?.Invoke();
        }
    }
}
