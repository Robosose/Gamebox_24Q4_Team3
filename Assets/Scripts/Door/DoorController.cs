using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Door
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] private DoorTrigger _doorTrigger;
        [SerializeField] private Transform _doorRotator;
        [SerializeField] private bool _isDoorOpen;
        
        [Space]
        [Header("Animation Params")]
        [SerializeField, Tooltip("Задаем радиус открывания двери")] private float _doorOpendRadius = -90;
        [SerializeField] private float _duration;
        [SerializeField] private float _timeBeforeCloseDoor;
        [SerializeField] private bool _isTutor;
        private Coroutine _cor;
        
        private void OnEnable()
        {
            _doorTrigger.onDoorTriggerEnter += SwitchDoorState;
        }
        
        private void OnDisable()
        {
            _doorTrigger.onDoorTriggerEnter -= SwitchDoorState;
        }
        
        private void SwitchDoorState()
        {
            if (!_isDoorOpen)
            {
                _doorRotator.DOLocalRotate(new Vector3(0, _doorOpendRadius, 0), _duration, RotateMode.Fast).onComplete += StartTimer;
                _isDoorOpen = !_isDoorOpen;
            }
        }

        private void StartTimer()
        {
            if(_cor != null)
                StopCoroutine(_cor);
            if(!_isTutor)
                _cor = StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(_timeBeforeCloseDoor);
            _doorRotator.DOLocalRotate(new Vector3(0, 0, 0), _duration);
            _isDoorOpen = false;
        }
    }
}
