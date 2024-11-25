using System.Collections;
using UnityEngine;

public class ScrimerTrigger : MonoBehaviour
{
    [SerializeField, Range(10, 30)] private float _lifeTime;

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
                LifeScrimers();
            }
        }
    }

    private IEnumerator LifeScrimers()
    {
        yield return new WaitForSeconds(_lifeTime);
        foreach (var scrimers in _scrimers)
        {
            scrimers.SetActive(false);
        }
    }
}
