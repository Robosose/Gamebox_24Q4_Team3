using UnityEngine;

public class TriggerMonsterOff : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
         other.gameObject.SetActive(false);
        }
    }
}
