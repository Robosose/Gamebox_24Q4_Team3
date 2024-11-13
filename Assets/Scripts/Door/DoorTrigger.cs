using System;
using UnityEngine;

namespace Door
{
    public class DoorTrigger : MonoBehaviour
    {
        public Action onDoorTriggerEnter;

        private void OnTriggerEnter(Collider other)
        {
            print(other.tag);
            if(other.CompareTag("Enemy"))
                onDoorTriggerEnter?.Invoke();
        }
    }
}