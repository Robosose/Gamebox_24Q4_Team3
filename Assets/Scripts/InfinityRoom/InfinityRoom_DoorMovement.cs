using System.Collections;
using UnityEngine;

public class InfinityRoom_DoorMovement : MonoBehaviour
{
    [SerializeField] private Transform _doorTransform;
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _timeBeforeNextPoint;

    private int _pointIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (_pointIndex < _points.Length)
        {
            StartCoroutine(MoveDoor());
        }
    }

    private IEnumerator MoveDoor()
    {
        var startTransform = _doorTransform.position;
        var f = 0f;
        while (f < 1f)
        {
            f += Time.deltaTime / _timeBeforeNextPoint;
            _doorTransform.position = Vector3.Lerp(startTransform, _points[_pointIndex].position, f);
            yield return null;
        }

        _pointIndex++;
    }
}
