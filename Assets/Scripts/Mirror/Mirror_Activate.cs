using UnityEngine;
using Zenject;

public class Mirror_Activate : MonoBehaviour
{
    [SerializeField] private float _activateTime;
    [SerializeField] private Transform _showTransform;
    [SerializeField] private Transform _removeTransform;

    private InputManager _inputManager;
    private float _mirrorProgress;

    [Inject]
    private void Construct(InputManager inputManager)
    {
        _inputManager = inputManager;
    }

    private void Update() => ActivateMirror(_inputManager.IsUsingMirror());

    private void ActivateMirror(bool activate)
    {
        var rotateX = Mathf.Lerp(_removeTransform.localEulerAngles.x, _showTransform.localEulerAngles.x, ActivateProgress(activate));
        
        transform.localPosition = Vector3.Lerp(_removeTransform.localPosition, _showTransform.localPosition, ActivateProgress(activate));
        transform.localEulerAngles = new Vector3(rotateX, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    private float ActivateProgress(bool activate)
    {
        if (activate)
            _mirrorProgress += Time.deltaTime / _activateTime;
        else
            _mirrorProgress -= Time.deltaTime / _activateTime;
            
        _mirrorProgress = Mathf.Clamp01(_mirrorProgress);
        return _mirrorProgress;
    }
    
    public bool Activated()
    {
        return _mirrorProgress >= 1f;
    }
}