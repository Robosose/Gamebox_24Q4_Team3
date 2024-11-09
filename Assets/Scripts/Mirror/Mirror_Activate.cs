using System.Collections;
using UnityEngine;
using Zenject;

public class Mirror_Activate : MonoBehaviour
{
    [SerializeField] private float _activateTime;
    [SerializeField] private Vector3 _showPosition;
    [SerializeField] private Vector3 _hidePosition;

    private InputManager _inputManager;
    private float _mirrorProgress;
    private bool _show;
    private Quaternion _baseRotation;
    
    [Inject]
    private void Construct(InputManager inputManager)
    {
        _inputManager = inputManager;
    }

    private void Start()
    {
        _baseRotation = transform.localRotation;
    }

    private void Update() => ActivateState();

    private void ActivateState()
    {
        if (_inputManager.IsUsingMirror())
        {
            if (_show)
                return;

            StopCoroutine(HideMirror());
            StartCoroutine(ShowMirror());
        }
        else
        {
            if (_mirrorProgress < 1f)
                return;

            StopCoroutine(ShowMirror());
            StartCoroutine(HideMirror());
        }
    }

    private IEnumerator ShowMirror()
    {
        _show = true;
        transform.localRotation = _baseRotation;
        while (_mirrorProgress < 1f)
        {
            _mirrorProgress = Mathf.Clamp01(_mirrorProgress + Time.deltaTime / _activateTime);
            transform.localPosition = Vector3.Lerp(_hidePosition, _showPosition, _mirrorProgress);
            yield return null;
        }
    }

    private IEnumerator HideMirror()
    {
        var lastPosition = transform.localPosition;
        while (_mirrorProgress > 0f)
        {
            _mirrorProgress = Mathf.Clamp01(_mirrorProgress - Time.deltaTime / _activateTime);
            transform.localPosition = Vector3.Lerp(_hidePosition, lastPosition, _mirrorProgress);
            yield return null;
        }

        _show = false;
    }

    public bool Activated()
    {
        return _mirrorProgress >= 1f;
    }
}