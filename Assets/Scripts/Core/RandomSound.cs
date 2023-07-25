using UnityEngine;

namespace RPG.Core
{
    public class RandomSound : MonoBehaviour
    {
        [SerializeField] AudioClip[] sounds;

        public void PlayRandomSound()
        {
            AudioClip clip = sounds[Random.Range(0, sounds.Length)];
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
    }
}