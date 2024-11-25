using System;
using UnityEngine;

namespace Bell
{
    public class BellSoundTrigger : MonoBehaviour
    {
        [SerializeField] private bool _isActive;

        public bool IsActive => _isActive;
        public event Action<Transform> OnBellSoundTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if(_isActive)
                return;
            if (other.CompareTag("Enemy"))
                _isActive = true;
        }

        public void OnSoundTriggered(Transform t)
        {
            if(_isActive)
                OnBellSoundTriggered?.Invoke(t);
        }
    }
}