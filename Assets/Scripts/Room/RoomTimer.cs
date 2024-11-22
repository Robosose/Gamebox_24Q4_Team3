using System.Collections;
using UnityEngine;

namespace Room
{
    public class RoomTimer : MonoBehaviour
    {
        [Tooltip("Кол-во секунд перед появлением врага")]
        [SerializeField] private float _secondsToEnemyStart;
        [SerializeField] private Enemy _enemy;

        [Header("Just For View")]
        [SerializeField] private float _seconds;
        
        private void Awake()
        {
            _seconds = _secondsToEnemyStart;
            StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            for (int i = 0; i < _secondsToEnemyStart; i++)
            {
                yield return new WaitForSeconds(1);
                _seconds--;
            }
            _enemy.gameObject.SetActive(true);
        }
    }
}