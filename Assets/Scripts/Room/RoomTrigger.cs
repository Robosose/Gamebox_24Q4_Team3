using System.Collections;
using UnityEngine;

namespace Room
{
    public class RoomTrigger : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private int _secondsToActiveEnemy;

        private bool _isActive;
        private Coroutine _cor;
        
        private void OnTriggerEnter(Collider other)
        {
            if(_cor is not null || _isActive)
                return;
            if (other.CompareTag("Player"))
                _cor = StartCoroutine(TimerToActive());
        }

        private IEnumerator TimerToActive()
        {
            yield return new WaitForSeconds(_secondsToActiveEnemy);
            _enemy.gameObject.SetActive(true);
        }
    }
}