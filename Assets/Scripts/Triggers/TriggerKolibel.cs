using UnityEngine;

public class TriggerKolibel : MonoBehaviour
{
    [SerializeField] GameObject kolibelPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          kolibelPrefab.gameObject.SetActive(false);
        }
    }
}
