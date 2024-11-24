using System.Collections;
using UnityEngine;

public class InfinityRoom_DoorMovement : MonoBehaviour
{
    [SerializeField] private Transform _doorTransform;
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _timeBeforeNextPoint;
    [SerializeField] private GameObject door;
    
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
        var t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / _timeBeforeNextPoint;
            _doorTransform.position = Vector3.Lerp(startTransform, _points[_pointIndex].position, t);
            
            if (_pointIndex + 1 == _points.Length)
            {
                door.transform.localRotation = Quaternion.Euler(door.transform.localRotation.x, Mathf.Lerp(0, 90, t),
                    door.transform.localRotation.z);
            }
            yield return null;
        }
        
        _pointIndex++;
    }

}
