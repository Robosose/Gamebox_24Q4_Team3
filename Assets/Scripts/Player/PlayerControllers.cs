using UnityEngine;
using Zenject;

public class PlayerControllers : MonoBehaviour
{
    [SerializeField] private float AnimBlendSpeed = 8.9f;
    [SerializeField] private Transform BoneRoot;
    [SerializeField] private Transform Camera;
    [SerializeField] private float UpperLimit = -40f;
    [SerializeField] private float BottomLimit = 40f;
    [SerializeField] private float mouseSensitivity = 10f;
    [SerializeField] private float _crouchSpeed = 0.7f;
    [SerializeField] private float _walkSpeed = 1f;

    public float UpperLimit1 => UpperLimit;
    public float BottomLimit1 => BottomLimit;
    
    private Rigidbody _playerRigidbody;
    private InputManager _inputManager;
    private Animator _animator;
    private bool _hasAnimator;

    private int _xVelHash;
    private int _yVelHash;
    private int _crouchHash;
    private int _handBlendHash;

    private float _xRotation;
    private Vector2 _currentVelocity;

    [Inject]
    private void Construct(InputManager inputManager)
    {
        _inputManager = inputManager;
    }

    private void Start()
    {
        _hasAnimator = TryGetComponent(out _animator);
        _playerRigidbody = GetComponent<Rigidbody>();

        _xVelHash = Animator.StringToHash("X_Velocity");
        _yVelHash = Animator.StringToHash("Y_Velocity");
        _crouchHash = Animator.StringToHash("Crouch");
        _handBlendHash = Animator.StringToHash("HandBlend");
    }

    private void FixedUpdate()
    {
        Move();
        HandleCrouch();
    }

    private void LateUpdate()
    {
        CamMovement();
        HandleMirror();
    }

    private void Move()
    {
        if (!_hasAnimator) return;

        float targetSpeed;
        if (_inputManager.IsCrouching())
        {
            targetSpeed = _crouchSpeed;
        }
        else
        {
            targetSpeed = _walkSpeed;
            //targetSpeed = _inputManager.IsSprinting() ? _runSpeed : _walkSpeed;
        }

        if (_inputManager.IsCrouching()) targetSpeed = 1f;

        if (_inputManager.GetPlayerMovement() == Vector2.zero) targetSpeed = 0;

        _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, _inputManager.GetPlayerMovement().x * targetSpeed,
            AnimBlendSpeed * Time.fixedDeltaTime);
        _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, _inputManager.GetPlayerMovement().y * targetSpeed,
            AnimBlendSpeed * Time.fixedDeltaTime);

        var xVelDifference = _currentVelocity.x - _playerRigidbody.angularVelocity.x;
        var zVelDifference = _currentVelocity.y - _playerRigidbody.angularVelocity.z;

        _playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, zVelDifference)),
            ForceMode.VelocityChange);

        _animator.SetFloat(_xVelHash, _currentVelocity.x);
        _animator.SetFloat(_yVelHash, _currentVelocity.y);
    }

    private void CamMovement()
    {
        if (!_hasAnimator) return;

        var mouseX = _inputManager.GetMouseDelta().x;
        var mouseY = _inputManager.GetMouseDelta().y;

        Camera.position = BoneRoot.position;

        if (_inputManager.IsRotatingMirror())
            return;
        _xRotation -= mouseY * mouseSensitivity * Time.smoothDeltaTime;
        _xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);

        Camera.localRotation = Quaternion.Euler(_xRotation, 0, 0);

        _playerRigidbody.MoveRotation(_playerRigidbody.rotation *
                                      Quaternion.Euler(0, mouseX * mouseSensitivity * Time.smoothDeltaTime, 0));
    }

    private void HandleCrouch()
    {
        _animator.SetBool(_crouchHash, _inputManager.IsCrouching());
    }

    private void HandleMirror()
    {
        if (!_hasAnimator) return;

        float targetValue = _inputManager.IsUsingMirror() ? 1f : 0f;
        float currentBlend = _animator.GetFloat(_handBlendHash);
        _animator.SetFloat(_handBlendHash, Mathf.Lerp(currentBlend, targetValue, Time.deltaTime * AnimBlendSpeed));

        float targetWeight = _inputManager.IsUsingMirror() ? 1f : 0f;
        float currentWeight = _animator.GetLayerWeight(1);
        _animator.SetLayerWeight(1, Mathf.Lerp(currentWeight, targetWeight, Time.deltaTime * AnimBlendSpeed));
    }
}