using System;
using System.Collections;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField] private Light _blinkingLight;
    [SerializeField] private float _blinkingInterval;
    [SerializeField] private int _blinkCount;

    private bool _isPlayerTrigger = false;

    private void Awake()
    {
        if (_blinkingLight == null)
        {
            enabled = false;
            throw new ArgumentNullException(paramName: nameof(gameObject.name), message: "Light reference is missing");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_isPlayerTrigger)
        {
            _isPlayerTrigger = true;
            StartCoroutine(BlinkLight());
        }
    }

    private IEnumerator BlinkLight()
    {
        int currentBlink = 0;
        while (currentBlink < _blinkCount)
        {
            _blinkingLight.enabled = !_blinkingLight.enabled;
            yield return new WaitForSeconds(_blinkingInterval);
            currentBlink++;
        }
        _blinkingLight.enabled = false;
    }
}
