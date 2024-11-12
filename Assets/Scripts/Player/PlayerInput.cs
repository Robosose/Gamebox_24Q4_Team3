using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController), typeof(BellSoundManager))]
public class PlayerInput : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField, Range(1f, 2f)] private float _walkSpeed;
    [SerializeField, Range(1.5f, 3f)] private float _sprintSpeed;
    [SerializeField, Range(0.5f, 1f)] private float _crouchSpeed;
    [SerializeField] private float _gravityValue = -9.81f;

    private Vector3 _playerVelocity;
    private CharacterController _characterController;
    private InputManager _inputManager;
    private Transform _cameraTransform;

    private BellSoundManager _bellSoundManager;
    private Vector2 _previousMousePosition;
    private float _mouseVelocity;
    
    private IMovementMode _currentMovementMode;
    private IMovementMode _walkMode;
    private IMovementMode _sprintMode;
    private IMovementMode _crouchMode;

    public Action LoudSound;

    [Inject]
    private void Construct(InputManager inputManager)
    {
        _characterController = GetComponent<CharacterController>();
        _bellSoundManager = GetComponent<BellSoundManager>();

        _cameraTransform = Camera.main.transform;
        _inputManager = inputManager;
        _walkMode = new WalkMode(_walkSpeed);
        _sprintMode = new SprintMode(_sprintSpeed);
        _crouchMode = new CrouchMode(_crouchSpeed);

        _currentMovementMode = _walkMode;
    }

    private void Update()
    {
        Move();
        Rotate();
        HandleBellSound();
    }

    private void Move()
    {
        SetMovementMode();
        //UpdateAnimation();
        

        var movement = _inputManager.GetPlayerMovement();
        var move = new Vector3(movement.x, 0f, movement.y);
        if (_currentMovementMode == _sprintMode && move != Vector3.zero)
            LoudSound?.Invoke();
        move = _cameraTransform.forward * move.z + _cameraTransform.right * move.x;
        move.y = 0f;

        _characterController.Move(move * _currentMovementMode.GetSpeed() * Time.deltaTime);

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    private void SetMovementMode()
    {
        if (_inputManager.IsSprinting())
            _currentMovementMode = _sprintMode;
        else if (_inputManager.IsCrouching())
            _currentMovementMode = _crouchMode;
        else
            _currentMovementMode = _walkMode;
    }

    private void Rotate()
    {
        var cameraFarward = _cameraTransform.forward;
        cameraFarward.y = 0f;
        if (!_inputManager.IsRotatingMirror())
            return;
        
        var камераВперед = _cameraTransform.forward;
        камераВперед.y = 0f;

        if (камераВперед.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(камераВперед);
            transform.rotation = targetRotation;
        }
    }

    private void HandleBellSound()
    {
        var currentMousePosition = _inputManager.GetMouseDelta();
        _mouseVelocity = (currentMousePosition - _previousMousePosition).magnitude / Time.deltaTime;

        if(_mouseVelocity > 10000)
        {
            _bellSoundManager.PlayBellSound(_mouseVelocity);
            LoudSound?.Invoke();
        }
        
        _previousMousePosition = currentMousePosition;
    }
}
