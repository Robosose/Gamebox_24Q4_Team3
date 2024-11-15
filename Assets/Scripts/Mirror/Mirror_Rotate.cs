using Patterns.States;
using UnityEngine;
using Zenject;

public class Mirror_Rotate : MonoBehaviour,IMirrorState
{
    [SerializeField] private float _baseRotationAngleX = 180f;
    [SerializeField] private float _baseRotationAngleY = 5f;
    [SerializeField] private float _boundariesRotationAngle = 30f;
    [SerializeField] private float _sensitivity = 5f;
    [SerializeField] private Mirror_Activate _mirrorActivate;
    
    private InputManager _inputManager;

    private float _mirrorRotationX;
    private float _mirrorRotationY;
    private Vector3 _baseRotation;

    [Inject]
    private void Construct(InputManager inputManager)
    {
        _inputManager = inputManager;
        _mirrorRotationX = _baseRotationAngleX;
        _mirrorRotationY = _baseRotationAngleY;
        _baseRotation = transform.eulerAngles;
    }

    private void SetRotate()
    {
        if (!_inputManager.IsRotatingMirror())
            return;

        var directionX = _inputManager.GetMouseDelta().x;
        var directionY = _inputManager.GetMouseDelta().y;
        
        _mirrorRotationX = Mathf.Clamp(_mirrorRotationX - directionX * _sensitivity * Time.deltaTime,
            _baseRotationAngleX - _boundariesRotationAngle, _baseRotationAngleX + _boundariesRotationAngle);
        
        _mirrorRotationY = Mathf.Clamp(_mirrorRotationY - directionY * _sensitivity * Time.deltaTime,
            _baseRotationAngleY - _boundariesRotationAngle, _baseRotationAngleY + _boundariesRotationAngle);
        
        transform.localRotation = Quaternion.Euler(_mirrorRotationY, _mirrorRotationX, _baseRotation.z);
    }

    public void Enter()
    {
        _mirrorRotationX = _baseRotationAngleX;
        _mirrorRotationY = _baseRotationAngleY;
    }

    public void Execute()
    {
        SetRotate();
    }

    public void FixedExecute()
    {
        
    }

    public void Exit()
    {
        
    }
}
