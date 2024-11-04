using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField, Range(1f, 2f)] private float _walkSpeed;
    [SerializeField, Range(1.5f, 3f)] private float _springSpeed;
    [SerializeField, Range(0.5f, 1f)] private float _crouchSpeed;
    [SerializeField] private float _gravityValue = -9.81f;

    private Vector3 _playerVelocity;

    private CharacterController _characterController;
    private InputManager _inputManager;
    private Transform _cameraTransform;

    [Inject]
    private void Construct(InputManager inputManager)
    {
        _characterController = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
        _inputManager = inputManager;
    }

    private void Update() => Move();
    
    private void Move()
    {
        var movement = _inputManager.GetPlayerMovement();
        var move = new Vector3(movement.x, 0f, movement.y);

        move = _cameraTransform.forward * move.z + _cameraTransform.right * move.x;
        move.y = 0f;

        _characterController.Move(move * _walkSpeed * Time.deltaTime);

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }
}
