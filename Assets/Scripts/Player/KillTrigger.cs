using System;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    [SerializeField] private Collider _collider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerDeath playerDeath))
        {
            playerDeath.IsDead(_collider.bounds.center);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out PlayerDeath playerDeath))
        {
            playerDeath.IsDead(_collider.bounds.center);
        }
    }
}
