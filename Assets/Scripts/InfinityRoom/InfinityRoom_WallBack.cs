using System.Collections;
using UnityEngine;

public class InfinityRoom_WallBack : MonoBehaviour
{
    [SerializeField] private float _timeBeforeTargetPoint;
    [SerializeField] private Transform _targetPoint;

    public void DoorMove()
    {
        StartCoroutine(DoorMovement());
    }

    private IEnumerator DoorMovement()
    {
        var startPosition = transform.position;
        var move = 0f;
        while (move < 1f)
        {
            move = Mathf.Clamp01(move + Time.deltaTime / _timeBeforeTargetPoint);
            transform.position =
                Vector3.Lerp(startPosition, _targetPoint.position, move);
            yield return null;
        }
    }
}