using System;
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
                _doorRotator.DOLocalRotate(new Vector3(0, _doorOpendRadius, 0), _duration, RotateMode.Fast).onComplete += () => print("Door Opened");
            else
                _doorRotator.DOLocalRotate(new Vector3(0, 0, 0), _duration, RotateMode.Fast);

            _isDoorOpen = !_isDoorOpen;
        }
    }
}
