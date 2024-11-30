using UnityEngine;

public class AudioTriggerActivator : MonoBehaviour
{
    [SerializeField] private GameObject[] audioSourceObjects;

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject audioObject in audioSourceObjects)
        {
          
            if (other.gameObject == audioObject)
            {
                AudioSource audioSource = audioObject.GetComponent<AudioSource>();
                if (audioSource != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                break;
            }
        }
    }
}
