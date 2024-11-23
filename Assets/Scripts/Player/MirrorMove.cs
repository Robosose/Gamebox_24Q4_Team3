using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

public class MirrorMove : MonoBehaviour
{
    [SerializeField] private Rig _rig;
    [SerializeField] private Transform _upperPointPosition;
    [SerializeField] private Transform _bottomPointPosition;
    [SerializeField] private Transform _upperCrouchPointPosition;
    [SerializeField] private Transform _bottomCrouchPointPosition;
    [SerializeField] private Transform _rightHandTarget;
    [SerializeField] private GameObject _camera;
    [SerializeField] private float _distanceBeforePlayer;
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
        _mirrorMove = 1;//_inputManager.IsUsingMirror()
            // ? Mathf.Clamp01(_mirrorMove + Time.deltaTime)
            // : Mathf.Clamp01(_mirrorMove - Time.deltaTime);

        _crouchValue = _inputManager.IsCrouching()
            ? Mathf.Clamp01(_crouchValue + Time.deltaTime / _crouchTime)
            : Mathf.Clamp01(_crouchValue - Time.deltaTime / _crouchTime);

        _rig.weight = _mirrorMove;
        var moveValue = Vector3.Lerp(
            Vector3.Lerp(_bottomPointPosition.localPosition, _bottomCrouchPointPosition.localPosition, _crouchValue),
            Vector3.Lerp(_upperPointPosition.localPosition, _upperCrouchPointPosition.localPosition, _crouchValue),
            MapValueToZeroToOne(_camera.transform.localEulerAngles.x, 50, -40));

        _rightHandTarget.localPosition = moveValue;
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