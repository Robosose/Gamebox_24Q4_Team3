using UnityEngine;
using Unity.Cinemachine;
using Zenject;

public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private float _clampAngle;

    private InputManager _inputManager;
    private Vector3 _startingRotation;

    [Inject]
    private void Construct(InputManager inputManager)
    {
        _inputManager = inputManager;
    }

    protected override void Awake()
    {
        base.Awake();

        _startingRotation = transform.localRotation.eulerAngles;
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage,
        ref CameraState state, float deltaTime)
    {
        if (vcam.Follow && stage is CinemachineCore.Stage.Aim)
        {
            if (_inputManager is null)
                return;

            var deltaInput = _inputManager.GetMouseDelta();

            _startingRotation.x +=
                deltaInput.x * (!_inputManager.IsRotatingMirror() ? _verticalSpeed : 0f) * Time.deltaTime;
            _startingRotation.y +=
                deltaInput.y * (!_inputManager.IsRotatingMirror() ? _horizontalSpeed : 0f) * Time.deltaTime;
            _startingRotation.y = Mathf.Clamp(_startingRotation.y, -_clampAngle, _clampAngle);

            state.RawOrientation = Quaternion.Euler(-_startingRotation.y, _startingRotation.x, 0f);
        }
    }
}