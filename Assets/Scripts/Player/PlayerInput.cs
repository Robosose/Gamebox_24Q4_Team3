using System;
using System.Linq;
using Bell;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField, Range(1f, 2f)] private float _walkSpeed;
    [SerializeField, Range(1.5f, 5f)] private float _sprintSpeed;
    [SerializeField, Range(0.5f, 1f)] private float _crouchSpeed;
    [SerializeField] private float _gravityValue = -9.81f;

    [Header("Free Camera Movement Settings")] 
    [SerializeField] private float _normalFlySpeed;
    [SerializeField] private float _slowFlySpeed;

    [Space] [SerializeField] private LayerMask _enemyMask;

    private Vector3 _playerVelocity;
    private CharacterController _characterController;
    private InputManager _inputManager;
    private Transform _cameraTransform;
    private BellSoundManager _bellSoundManager;
    private PlayerView _playerView;

    private Vector2 _previousMousePosition;
    private float _mouseVelocity;
    private bool _freeCamera;
    
    private IMovementMode _currentMovementMode;
    private IMovementMode _walkMode;
    private IMovementMode _sprintMode;
    private IMovementMode _crouchMode;

    private BellSoundTrigger _trigger;

    [Inject]
    private void Construct(InputManager inputManager, BellSoundTrigger trigger)
    {
        _characterController = GetComponent<CharacterController>();
        _playerView = GetComponent<PlayerView>();
        _bellSoundManager = GetComponent<BellSoundManager>();
        _cameraTransform = Camera.main.transform;
        _inputManager = inputManager;
        _bellSoundManager = GetComponent<BellSoundManager>();
        _walkMode = new WalkMode(_walkSpeed);
        _sprintMode = new SprintMode(_sprintSpeed);
        _crouchMode = new CrouchMode(_crouchSpeed);

        _currentMovementMode = _walkMode;
        _trigger = trigger;
    }

    private void Update()
    {
        SetMoveState();
        Move();
        FreeMove();
        Rotate();
        HandleBellSound();
    }

    private void SetMoveState()
    {
        if (_inputManager.FreeCameraActivate())
        {
            _freeCamera = !_freeCamera;
            _characterController.enabled = !_freeCamera;
        }
    }

    private void Move()
    {
        SetMovementMode();
        UpdateAnimation();
        
        if(_freeCamera)
            return;
        
        var movement = _inputManager.GetPlayerMovement();
        var move = new Vector3(movement.x, 0f, movement.y);

        if (_currentMovementMode == _sprintMode && move != Vector3.zero)
            _trigger.OnSoundTriggered(transform);


        move = _cameraTransform.forward * move.z + _cameraTransform.right * move.x;
        move.y = 0f;
        
        _characterController.Move(_playerVelocity * Time.deltaTime);
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _characterController.Move(move * (_currentMovementMode.GetSpeed() * Time.deltaTime));
    }


    private void FreeMove()
    {
        if (!_freeCamera)
            return;

        var inputDirection = _inputManager.GetPlayerMovement();
        var move = new Vector3(inputDirection.x, _inputManager.FreeCameraFly(), inputDirection.y);
        var moveDirection = _cameraTransform.forward * move.z + _cameraTransform.right * move.x +
                            _cameraTransform.up * move.y;

        var speed = (!_inputManager.IsCrouching() ? _normalFlySpeed : _slowFlySpeed) * Time.deltaTime;
        transform.position += moveDirection * speed;
    }

    private void SetMovementMode()
    {
        // // if (_inputManager.IsSprinting())
        // //     _currentMovementMode = _sprintMode;
        //
        // else if (_inputManager.IsCrouching())
        //     _currentMovementMode = _crouchMode;
        // else
        //     _currentMovementMode = _walkMode;
    }

    private void UpdateAnimation()
    {
        var movement = _inputManager.GetPlayerMovement();

        bool isMoving = movement != Vector2.zero;
        bool isSprinting = _currentMovementMode == _sprintMode;
        bool isCrouching = _currentMovementMode == _crouchMode;

        _playerView.SetWalk(isMoving);
        _playerView.SetSprint(isMoving && isSprinting);
        _playerView.SetCrouch(isCrouching);
        _playerView.SetWalkCrouch(isMoving && isCrouching);
    }

    private void Rotate()
    {
        if (!_inputManager.IsRotatingMirror())
            return;
        
        var cameraForward = _cameraTransform.forward;
        cameraForward.y = 0f;

        if (cameraForward.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = targetRotation;
        }
    }

    private void HandleBellSound()
    {
        var currentMousePosition = _inputManager.GetMouseDelta();
        _mouseVelocity = (currentMousePosition - _previousMousePosition).magnitude / Time.deltaTime;

        if (_mouseVelocity > 10000)
        {
            _bellSoundManager.PlayBellSound(_mouseVelocity);
            _trigger.OnSoundTriggered(transform);
        }

        _previousMousePosition = currentMousePosition;
    }
}
