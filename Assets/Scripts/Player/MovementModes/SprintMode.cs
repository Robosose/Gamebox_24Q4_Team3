public class SprintMode : IMovementMode
{
    private readonly float _sprintSpeed;

    public SprintMode(float springSpeed)
    {
        _sprintSpeed = springSpeed;
    }

    public float GetSpeed() => _sprintSpeed;
}
