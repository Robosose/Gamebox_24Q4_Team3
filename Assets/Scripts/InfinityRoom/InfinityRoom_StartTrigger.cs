using UnityEngine;

public class InfinityRoom_StartTrigger : MonoBehaviour
{
    [SerializeField] private InfinityRoom_WallBack _wallBack;
    [SerializeField] private ParticleSystem _particleSystem;
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
            return;

        _wallBack.DoorMove();
        gameObject.SetActive(false);
    }
}
