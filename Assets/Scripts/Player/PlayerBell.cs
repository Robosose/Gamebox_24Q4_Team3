using System;
using System.Collections;
using Bell;
using Enums;
using Player;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class PlayerBell : MonoBehaviour
{
    [SerializeField] private float flickThreshold;
    [SerializeField] private AudioSource bellSource;
    [SerializeField] private float delayBeforeNextBell;
    [SerializeField] private int bellCallCount;
    [SerializeField] private Voice voice;
    [SerializeField] private PlayerVignette playerVignette;
    [Range(0f, 1f)] [SerializeField] private float phraseChance;
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

    private IEnumerator Start()
    {
        _isBellCalled = true;
        yield return new WaitForSeconds(2f);
        _isBellCalled = false;
    }

    private void Update()
    {
        if (playerVignette.SilenceTime <= 0)
            _bellCounter = 0;
        
        if(_isBellCalled)
            return;
        
        if(_inputManager.IsRotatingMirror() && _inputManager.IsUsingMirror())
            return;
        
        Vector2 currentMousePosition = _inputManager.GetMouseDelta();
        Vector2 delta = currentMousePosition - _previousMousePosition;

        if (delta.magnitude > flickThreshold)
        {
            Call?.Invoke();
            StartCoroutine(CallBell());
        }
        _previousMousePosition = Vector2.zero;
    }

    private IEnumerator CallBell()
    {
        _isBellCalled = true;
        _bellCounter++;
        if(Random.value<phraseChance)
            voice.ChosePhrase(PhrasesType.OhBell);
        bellSource.Play();
        
        if (_bellCounter >= bellCallCount)
        {
            _bellSoundTrigger.OnSoundTriggered(transform);
            _bellCounter = 0;
        }
        yield return new WaitForSeconds(delayBeforeNextBell);
        _isBellCalled = false;
    }
}
