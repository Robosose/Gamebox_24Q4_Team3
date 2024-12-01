using System.Collections;
using UnityEngine;

public class InfinityRoom_WallBack : MonoBehaviour
{
    [SerializeField] private float _timeBeforeTargetPoint;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private AudioSource _source;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Collider _wallCollider;

    public void DoorMove()
    {
        StartCoroutine(DoorMovement());
        _wallCollider.enabled = true;
        _particleSystem.gameObject.SetActive(true);
        _source.Play();
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