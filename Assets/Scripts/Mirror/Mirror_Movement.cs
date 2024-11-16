using Patterns.States;
using UnityEngine;
using Zenject;

public class Mirror_Movement : MonoBehaviour,IMirrorState
{
    [SerializeField] private float _sensitivity;

    private InputManager _inputManager;

    [Inject]
    private void Construct(InputManager inputManager)
    {
        _inputManager = inputManager;
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