using UnityEngine;

namespace Core.Lessons.Code023Events.Scripts
{
	public class PlayerTrigger : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			Coin coin = other.GetComponent<Coin>();
			
			if (coin)
			{
				UserInterfaceManager.Instance.OnCoinCollected?.Invoke(coin.Name);
				Destroy(coin.gameObject);
			}
		}
	}
}