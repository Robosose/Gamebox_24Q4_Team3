public class WalkMode : IMovementMode
{
    private readonly float _walkSpeed;

    public WalkMode(float walkSpeed)
    {
        _walkSpeed = walkSpeed;
    }

    public float GetSpeed() => _walkSpeed;
}
