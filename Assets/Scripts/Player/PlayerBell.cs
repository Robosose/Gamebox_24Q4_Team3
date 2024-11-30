using System;
using System.Collections;
using Bell;
using UnityEngine;
using Zenject;

public class PlayerBell : MonoBehaviour
{
    [SerializeField] private float flickThreshold;
    [SerializeField] private AudioSource bellSource;
    [SerializeField] private float delayBeforeNextBell;
    [SerializeField] private int _bellCallCount;
    
    private Vector2 _previousMousePosition;
    private InputManager _inputManager;
    private BellSoundTrigger _bellSoundTrigger;
    public Action Call;
    private bool _isBellCalled;
    private int _bellCounter;

    [Inject]
    private void Construct(InputManager inputManager,BellSoundTrigger bellSoundTrigger)
    {
        _inputManager = inputManager;
        _bellSoundTrigger = bellSoundTrigger;
    }

    private void Start()
    {
        bellSource.Play();
        bellSource.Pause();
    }

    private void Update()
    {
        if(_isBellCalled)
            return;
        
        if(_inputManager.IsRotatingMirror())
            return;
        
        Vector2 currentMousePosition = _inputManager.GetMouseDelta();
        Vector2 delta = currentMousePosition - _previousMousePosition;

        if (delta.magnitude > flickThreshold)
        {
            Call?.Invoke();
            _bellCounter++;
            StartCoroutine(CallBell());
        }
        _previousMousePosition = Vector2.zero;
    }

    private IEnumerator CallBell()
    {
        _isBellCalled = true;
        bellSource.Play();
        if (_bellCounter >= _bellCallCount)
        {
            _bellSoundTrigger.OnSoundTriggered(transform);
            _bellCounter = 0;
        }
        yield return new WaitForSeconds(delayBeforeNextBell);
        _isBellCalled = false;
    }
}
