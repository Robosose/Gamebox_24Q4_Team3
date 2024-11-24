using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class InfinityRoom_DoorMovement : MonoBehaviour
{
    [SerializeField] private Transform _doorTransform;
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _timeBeforeNextPoint;
    [SerializeField] private GameObject door;

    [Header("Material")] 
    [SerializeField] private Transform _startTranslateMaterialPoint;
    [SerializeField] private Transform _endTranslateMaterialPoint;
    [SerializeField] private MeshRenderer _doorMeshRenderer;

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

    private void Update()
    {
        float tZ = Mathf.Clamp01((transform.position.z - _startTranslateMaterialPoint.position.z) /
                   (_endTranslateMaterialPoint.position.z - _startTranslateMaterialPoint.position.z));
        _doorMeshRenderer.material.SetFloat("_DirtyValue",tZ);
    }
}