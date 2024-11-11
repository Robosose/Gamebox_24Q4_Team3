using System;
using Patterns.States;
using UnityEngine;
using Zenject;

public class Mirror_Movement : MonoBehaviour,IMirrorState
{
    [SerializeField] private float _sensitivity;

    private InputManager _inputManager;
    private Vector3 _baseRotation;

    [Inject]
    private void Construct(InputManager inputManager)
    {
        _inputManager = inputManager;
    }

    private void Start()
    {
        _baseRotation = transform.eulerAngles;
    }

    private void Move()
    {
        if (_inputManager.IsRotatingMirror() && _inputManager.IsUsingMirror())
        {
            transform.localPosition += (Vector3)_inputManager.GetMouseDelta()* (_sensitivity * Time.deltaTime);
        }
    }

    public void Enter()
    {
        _baseRotation = transform.eulerAngles;
    }

    public void Execute()
    {
        Move();
    }

    public void FixedExecute()
    {
        
    }

    public void Exit()
    {
        
    }
}