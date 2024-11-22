using UnityEngine;

namespace Room
{
    public class RoomTrigger : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
                _enemy.gameObject.SetActive(true);
        }
    }
}