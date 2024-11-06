using UnityEngine;
using Zenject;

public class Mirror_Rotate : MonoBehaviour
{
    [SerializeField] private float _baseRotationAngle = 180f;
    [SerializeField] private float _boundariesRotationAngle = 30f;
    [SerializeField] private float _sensitivity = 5f;
    [SerializeField] private Mirror_Activate _mirrorActivate;
    
    private InputManager _inputManager;

    private float _mirrorRotation;
    private Vector3 _baseRotation;

    [Inject]
    private void Construct(InputManager inputManager)
    {
        _inputManager = inputManager;
        _mirrorRotation = _baseRotationAngle;
        _baseRotation = transform.eulerAngles;
    }

    private void Update() => SetRotate();

    private void SetRotate()
    {
        if (!_mirrorActivate.Activated())
            return;
        
        if (!_inputManager.IsRotatingMirror())
            return;

        var direction = _inputManager.GetMouseDelta().x;
        
        _mirrorRotation = Mathf.Clamp(_mirrorRotation - direction * _sensitivity * Time.deltaTime,
            _baseRotationAngle - _boundariesRotationAngle, _baseRotationAngle + _boundariesRotationAngle);

        transform.localRotation = Quaternion.Euler(_baseRotation.x, _mirrorRotation, _baseRotation.z);
    }
}
