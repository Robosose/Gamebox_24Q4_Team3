using System.Collections;
using UnityEngine;

public class InfinityRoom_WallBack : MonoBehaviour
{
    [SerializeField] private float _timeBeforeTargetPoint;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private AudioSource _source;
    [SerializeField] private ParticleSystem _particleSystem;

    public void DoorMove()
    {
        StartCoroutine(DoorMovement());
        _particleSystem.gameObject.SetActive(true);
        _source.Play();
        _source.loop = true;
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