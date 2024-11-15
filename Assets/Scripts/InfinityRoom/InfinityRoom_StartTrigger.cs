using UnityEngine;

public class InfinityRoom_StartTrigger : MonoBehaviour
{
    [SerializeField] private InfinityRoom_WallBack _wallBack;
    [SerializeField] private GameObject _wallCollider;
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
            return;
        
        _wallCollider.SetActive(true);
        _wallBack.DoorMove();
        gameObject.SetActive(false);
    }
}
