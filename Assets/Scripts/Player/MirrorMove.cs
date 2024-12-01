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
    [SerializeField] private float _leftBorder;
    [SerializeField] private float _rightBorder;
    [SerializeField] private float _border;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _crouchTime;
    [SerializeField] private PlayerControllers _playerControllers;
    [SerializeField] private LayerMask _collisionLayers;

    private InputManager _inputManager;
    private float _rotateX;
    private float _rotateY;
    private float _baseRotateX;
    private float _baseRotateY;
    private float _mirrorMove;
    private float _crouchValue;
    private float _handDistance;

    [Inject]
    private void Construct(InputManager inputManager)
    {
        _inputManager = inputManager;
        _baseRotateX = _rightHandTarget.localEulerAngles.x;
        _baseRotateY = _rightHandTarget.localEulerAngles.y;
    }

    private void Start()
    {
        _handDistance = GetFarthestDistance(_upperPointPosition, _upperCrouchPointPosition, _bottomPointPosition,
            _bottomCrouchPointPosition);
    }

    private void Update()
    {
        GetHand();
        HandRotate();
    }

    private void GetHand()
    {
        _mirrorMove = _inputManager.IsUsingMirror()
            ? Mathf.Clamp01(_mirrorMove + Time.deltaTime)
            : Mathf.Clamp01(_mirrorMove - Time.deltaTime);

        _crouchValue = _inputManager.IsCrouching()
            ? Mathf.Clamp01(_crouchValue + Time.deltaTime / _crouchTime)
            : Mathf.Clamp01(_crouchValue - Time.deltaTime / _crouchTime);

        _rig.weight = _mirrorMove;
        //_rig.weight = 1;

        var moveValue = Vector3.Lerp(
            Vector3.Lerp(_bottomPointPosition.localPosition, _bottomCrouchPointPosition.localPosition, _crouchValue),
            Vector3.Lerp(_upperPointPosition.localPosition, _upperCrouchPointPosition.localPosition, _crouchValue),
            MapValueToZeroToOne(_camera.transform.localEulerAngles.x, _playerControllers.BottomLimit1,
                _playerControllers.UpperLimit1));

        _rightHandTarget.localPosition = CheckForCollision(transform.position, moveValue);
    }

    private void HandRotate()
    {
        if (_inputManager.IsRotatingMirror() && _inputManager.IsUsingMirror())
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

    private Vector3 CheckForCollision(Vector3 currentPosition, Vector3 targetPosition)
    {
        var direction = new Vector3(targetPosition.x, 0, targetPosition.z) -
                        new Vector3(currentPosition.x, 0, currentPosition.z);
        
        if (Physics.Raycast(currentPosition, transform.forward.normalized, out RaycastHit hit, _handDistance,
                _collisionLayers))
        {
            float hitInfoDistance = _handDistance - hit.distance;
            return targetPosition - direction.normalized * hitInfoDistance;
        }

        return targetPosition;
    }

    private float GetFarthestDistance(Transform point1, Transform point2, Transform point3, Transform point4)
    {
        Vector3[] points = { point1.localPosition, point2.localPosition, point3.localPosition, point4.localPosition };
        float maxDistance = 0;

        foreach (var point in points)
        {
            float distance = Mathf.Pow(point.x, 2) + Mathf.Pow(point.z, 2);
            float distanceSqrt = Mathf.Sqrt(distance);

            if (distanceSqrt > maxDistance)
            {
                maxDistance = distanceSqrt;
            }
        }

        return maxDistance;
    }

    float MapValueToZeroToOne(float x, float minIn, float maxIn)
    {
        var newValueX = x < 180f ? x : -(360f - x);
        return (newValueX - minIn) / (maxIn - minIn);
    }
}
