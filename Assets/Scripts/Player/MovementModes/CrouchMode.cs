public class CrouchMode : IMovementMode
{
    private readonly float _crouchSpeed;

    public CrouchMode(float crouchSpeed)
    {
        _crouchSpeed = crouchSpeed;
    }

    public float GetSpeed() => _crouchSpeed;
}
