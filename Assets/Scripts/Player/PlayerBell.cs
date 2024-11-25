using System.Collections;
using Bell;
using UnityEngine;
using Zenject;

public class PlayerBell : MonoBehaviour
{
    [SerializeField] private float flickThreshold;
    [SerializeField] private AudioSource bellSource;
    [SerializeField] private float delayBeforeNextBell;
    
    private Vector2 _previousMousePosition;
    
    private InputManager _inputManager;
    private BellSoundTrigger _bellSoundTrigger;
    private bool _isBellCalled;

    [Inject]
    private void Construct(InputManager inputManager,BellSoundTrigger bellSoundTrigger)
    {
        _inputManager = inputManager;
        _bellSoundTrigger = bellSoundTrigger;
    }
    
    private void Update()
    {
        if(_isBellCalled)
            return;
        Vector2 currentMousePosition = _inputManager.GetMouseDelta();
        Vector2 delta = currentMousePosition - _previousMousePosition;

        if (delta.magnitude > flickThreshold)
        {
            StartCoroutine(CallBell());
        }

        _previousMousePosition = currentMousePosition;
    }

    private IEnumerator CallBell()
    {
        _isBellCalled = true;
        _bellSoundTrigger.OnSoundTriggered(transform);
        bellSource.Play();
        yield return new WaitForSeconds(delayBeforeNextBell);
        _isBellCalled = false;
    }
}
