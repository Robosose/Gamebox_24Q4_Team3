using System.Collections;
using UnityEngine;

public class ScrimerTrigger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Range(1, 10)] private float _lifeTime;
    [SerializeField] private GameObject[] _scrimers;
    [SerializeField] private AudioSource _audioSource;

    private bool _isTriggered;

    void Start()
    {
        foreach (var scrimers in _scrimers)
        {
            scrimers.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_isTriggered)
        {
            _isTriggered = true;
            ActivateScreamers();
            StartCoroutine(DeactivateScreamersAfterDeley());
        }
    }

    private void ActivateScreamers()
    {
        foreach (var scrimers in _scrimers)
        {
            scrimers.SetActive(true);
            _audioSource.Play();
        }
    }

    private IEnumerator DeactivateScreamersAfterDeley()
    {
        yield return new WaitForSeconds(_lifeTime);

        foreach (var scrimers in _scrimers)
        {
            scrimers.SetActive(false);
        }

        Destroy(gameObject);
    }
}
