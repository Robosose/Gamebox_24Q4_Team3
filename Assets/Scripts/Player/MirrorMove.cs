using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Serialization;
using Zenject;

public class MirrorMove : MonoBehaviour
{
    [SerializeField] private Rig _rig;
    [SerializeField] private Transform _rightHandTarget;
    [SerializeField] private GameObject _camera;
    [SerializeField] private float _distanceBeforePlayer;
    [SerializeField] private float _upperPosition;
    [SerializeField] private float _bottomPosition;
    [SerializeField] private float _upperCrouchPosition;
    [SerializeField] private float _bottomCrouchPosition;
    [SerializeField] private float _leftBorder;
    [SerializeField] private float _rightBorder;
    [SerializeField] private float _border;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _crouchTime;
    private InputManager _inputManager;
    private float _rotateX;
    private float _rotateY;
    private float _baseRotateX;
    private float _baseRotateY;
    private float _mirrorMove;
    private float _crouchValue;

    [Inject]
    private void Construct(InputManager inputManager)
    {
        _inputManager = inputManager;
        _baseRotateX = _rightHandTarget.localEulerAngles.x;
        _baseRotateY = _rightHandTarget.localEulerAngles.y;
    }

    private void Update()
    {
        _mirrorMove = _inputManager.IsUsingMirror()
            ? Mathf.Clamp01(_mirrorMove + Time.deltaTime)
            : Mathf.Clamp01(_mirrorMove - Time.deltaTime);

        _crouchValue = _inputManager.IsCrouching()
            ? Mathf.Clamp01(_crouchValue + Time.deltaTime / _crouchTime)
            : Mathf.Clamp01(_crouchValue - Time.deltaTime / _crouchTime);

        _rig.weight = _mirrorMove;
        var moveValue = Mathf.Lerp(Mathf.Lerp(_bottomPosition, _bottomCrouchPosition, _crouchValue),
            Mathf.Lerp(_upperPosition, _upperCrouchPosition, _crouchValue),
            MapValueToZeroToOne(_camera.transform.localEulerAngles.x, 50, -40));
        _rightHandTarget.localPosition =
            new Vector3(_rightHandTarget.localPosition.x, moveValue, _rightHandTarget.localPosition.z);
        
        if (_inputManager.IsRotatingMirror())
        {
            _rotateX = Mathf.Clamp(_rotateX + _inputManager.GetMouseDelta().x * Time.deltaTime * _sensitivity,
                _baseRotateX - _leftBorder,
                _baseRotateX + _rightBorder);
            _rotateY = Mathf.Clamp(_rotateY + _inputManager.GetMouseDelta().y * Time.deltaTime * _sensitivity,
                _baseRotateY - _border,
                _baseRotateY + _border);

            _rightHandTarget.localEulerAngles = new Vector3(_rotateY, -_rotateX, _rightHandTarget.localEulerAngles.z);
        }
    }

    float MapValueToZeroToOne(float x, float minIn, float maxIn)
    {
        var newValueX = x < 180f ? x : -(360f - x);
        return (newValueX - minIn) / (maxIn - minIn);
    }
}