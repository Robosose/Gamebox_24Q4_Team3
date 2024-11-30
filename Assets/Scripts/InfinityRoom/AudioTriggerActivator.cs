using System;
using UnityEngine;

public class AudioTriggerActivator : MonoBehaviour
{
    [SerializeField] private GameObject[] audioSourceObjects;

    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AudioTrigger trigger))
        {
            //print(trigger.gameObject.name);
        }
    }
}
