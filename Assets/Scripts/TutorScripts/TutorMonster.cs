using UnityEngine;

public class TutorMonster : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private AudioSource _audioSourceStep;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            _audioSourceStep.Stop();
            enemy.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
