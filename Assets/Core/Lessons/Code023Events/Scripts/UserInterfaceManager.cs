using System;
using TMPro;
using UnityEngine;

namespace Core.Lessons.Code023Events.Scripts
{
	public class UserInterfaceManager : MonoBehaviour
	{
		public static UserInterfaceManager Instance { get; private set; }

		public Action<string> OnCoinCollected;

		[SerializeField] private TMP_Text _info;
		
		private void Awake()
		{
			Instance = this;
		}

		private void OnEnable()
		{
			OnCoinCollected += CoinCollected;
		}

		private void OnDisable()
		{
			OnCoinCollected -= CoinCollected;
		}

		private void CoinCollected(string coinName)
		{
			_info.text = $"Вы подобрали: {coinName}";
		}
	}
}