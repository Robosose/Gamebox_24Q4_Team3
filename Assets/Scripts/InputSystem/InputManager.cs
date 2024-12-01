using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputSystem_Actions _action;

    private void Awake()
    {
        _action = new InputSystem_Actions();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        ActionEnable();
    }

    private void OnDisable()
    {
        ActionDisable();
    }

    public void ActionEnable()
    {
        _action.Enable();
    }
    
    public void ActionDisable()
    {
        _action.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return _action.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return _action.Player.Look.ReadValue<Vector2>();
    }

    public bool IsCrouching() => _action.Player.Crouch.ReadValue<float>() > 0;
    public bool IsUsingMirror() => _action.Player.UseMirror.ReadValue<float>() > 0;
    public bool IsRotatingMirror() => _action.Player.RotateMirror.ReadValue<float>() > 0;
    public bool FreeCameraActivate() => _action.Player.FreeCameraActivate.triggered;
    public float FreeCameraFly() => _action.Player.FreeCameraFlyUp.ReadValue<float>();
    public bool IsPause() => _action.UI.Pause.triggered;
}