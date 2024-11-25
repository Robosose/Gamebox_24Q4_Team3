using UnityEngine;

public class ScrimerTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] _scrimers;

    void Start()
    {
        foreach (var scrimers in _scrimers)
        {
            scrimers.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            foreach (var scrimers in _scrimers)
            {
                scrimers.SetActive(true);
            }
        }
    }
}
