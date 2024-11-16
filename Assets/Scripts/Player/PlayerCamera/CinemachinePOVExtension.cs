using UnityEngine;
using Unity.Cinemachine;
using Zenject;

public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private float _clampAngle;
    [SerializeField] private float _standingHeight;
    [SerializeField] private float _crouchingHeight;
    [SerializeField] private float _heigtTransitionSpeed;

    [SerializeField] private Transform _cameraHolder;

    private InputManager _inputManager;
    private Vector3 _startingRotation;
    private float _targetHeight;

    [Inject]
    private void Construct(InputManager inputManager)
    {
        _inputManager = inputManager;
    }

    protected override void Awake()
    {
        base.Awake();

        _startingRotation = transform.localRotation.eulerAngles;
        _targetHeight = _standingHeight;
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage,
        ref CameraState state, float deltaTime)
    {
        if (vcam.Follow && stage is CinemachineCore.Stage.Aim)
        {
            if (_inputManager is null)
                return;

            var deltaInput = _inputManager.GetMouseDelta();

            _startingRotation.x += deltaInput.x *
                                   (!(_inputManager.IsRotatingMirror() && _inputManager.IsUsingMirror()) ? _verticalSpeed : 0f) *
                                   Time.deltaTime;
            _startingRotation.y += deltaInput.y *
                                   (!(_inputManager.IsRotatingMirror() && _inputManager.IsUsingMirror()) ? _horizontalSpeed : 0f) *
                                   Time.deltaTime;
            _startingRotation.y = Mathf.Clamp(_startingRotation.y, -_clampAngle, _clampAngle);

            state.RawOrientation = Quaternion.Euler(-_startingRotation.y, _startingRotation.x, 0f);

            UpdateCameraHeight(deltaTime);
        }
    }

    private void UpdateCameraHeight(float deltaTime)
    {
        if (_inputManager.IsCrouching())
            _targetHeight = _crouchingHeight;
        else
            _targetHeight = _standingHeight;

        var position = _cameraHolder.localPosition;
        position.y = Mathf.Lerp(position.y, _targetHeight, _heigtTransitionSpeed * deltaTime);
        _cameraHolder.localPosition = position;
    }
}