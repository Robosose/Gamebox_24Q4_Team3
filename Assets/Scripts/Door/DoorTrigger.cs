using System;
using UnityEngine;

namespace Door
{
    public class DoorTrigger : MonoBehaviour
    {
        public Action onDoorTriggerEnter;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Enemy"))
                onDoorTriggerEnter?.Invoke();
        }
    }
}