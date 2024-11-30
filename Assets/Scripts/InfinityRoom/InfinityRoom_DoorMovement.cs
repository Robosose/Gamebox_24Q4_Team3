using System.Collections;
using UnityEngine;

public class InfinityRoom_DoorMovement : MonoBehaviour
{
    [SerializeField] private Transform _doorTransform;
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _timeBeforeNextPoint;
    [SerializeField] private GameObject door;
    [SerializeField] private float velocityOpenDoor;
    [SerializeField] private int maxTriggerCount = 3;
    [SerializeField] private InfinityRoom_WallBack _wallBack;
    [SerializeField] private Collider doorCollider;
    [Header("Material")] 
    [SerializeField] private Transform _startTranslateMaterialPoint;
    [SerializeField] private Transform _endTranslateMaterialPoint;
    [SerializeField] private MeshRenderer _doorMeshRenderer;

    private int _triggerCounter;
    private Material _doorMaterial;
    private int _pointIndex;

    private void Start()
    {
        _doorMaterial = _doorMeshRenderer.material;
        _doorMeshRenderer.material = _doorMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        _triggerCounter++;
        if (_triggerCounter >= maxTriggerCount)
        {
            if(!doorCollider)
                doorCollider.enabled = false;
            _wallBack.DoorMove();
            
            if (_pointIndex < _points.Length)
            {
                StartCoroutine(MoveDoor());
                GetComponent<Collider>().enabled = false;
            }
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
            
            yield return null;
        }
        
        GetComponent<Collider>().enabled = true;
        _pointIndex++;
        
        if (_pointIndex == _points.Length)
        {
            StartCoroutine(OpenDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        var t = 0f;
        while (t <=1)
        {
            t = Mathf.Clamp01(t + Time.deltaTime/velocityOpenDoor);
            door.transform.localRotation = Quaternion.Euler(door.transform.localRotation.x, Mathf.Lerp(0, 90, t),
                door.transform.localRotation.z);
            yield return null;
        }
    }

    private void Update()
    {
        float tZ = Mathf.Clamp01((transform.position.z - _startTranslateMaterialPoint.position.z) /
                   (_endTranslateMaterialPoint.position.z - _startTranslateMaterialPoint.position.z));
        _doorMeshRenderer.material.SetFloat("_DirtyValue",tZ);
    }
}