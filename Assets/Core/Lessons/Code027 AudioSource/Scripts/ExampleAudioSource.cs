using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Lessons.Code027_AudioSource.Scripts
{
    public class ExampleAudioSource : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip _audioClip;

        public void OnClickPlayHit()
        {
            _audioSource.pitch = Random.Range(0.8f, 0.9f);
            _audioSource.Play();
        }
    }
}
