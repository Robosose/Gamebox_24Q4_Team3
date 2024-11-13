using UnityEngine;

public class InfinityRoom_WallBack : MonoBehaviour
{
    [SerializeField] private float _wallSpeed;
    [SerializeField] private Direction _direction = Direction.Forward;
    
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        Forward,
        Back
    }

    private void Update()
    {
        transform.position += MoveDirection() * (_wallSpeed * Time.deltaTime);
    }

    private Vector3 MoveDirection()
    {
        switch (_direction)
        {
            case Direction.Up:
                return Vector3.up;
            case Direction.Down:
                return Vector3.down;
            case Direction.Left:
                return Vector3.left;
            case Direction.Right:
                return Vector3.right;
            case Direction.Forward:
                return Vector3.forward;
            case Direction.Back:
                return Vector3.back;
        }
        return Vector3.zero;
    }
}
