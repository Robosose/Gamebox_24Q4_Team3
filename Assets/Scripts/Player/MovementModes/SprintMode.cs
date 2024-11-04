public class SprintMode : IMovementMode
{
    private readonly float _springSpeed;

    public SprintMode(float springSpeed)
    {
        _springSpeed = springSpeed;
    }

    public float GetSpeed() => _springSpeed;
}
