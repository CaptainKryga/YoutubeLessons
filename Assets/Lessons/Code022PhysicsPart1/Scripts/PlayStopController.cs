using System;
using TMPro;
using UnityEngine;

public class PlayStopController : MonoBehaviour
{
    [SerializeField] private TMP_Text _btnText;

    public void OnToggle(bool value)
    {
        _btnText.text = value ? "Выключить" : "Включить";

        Physics.autoSimulation = value;
    }
}
