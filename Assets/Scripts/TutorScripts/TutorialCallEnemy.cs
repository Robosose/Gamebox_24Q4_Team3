using System.Collections;
using UnityEngine;

public class TutorialCallEnemy : MonoBehaviour
{
    [SerializeField] private float lookTime;
    [SerializeField] private float timeBeforeSpawnEnemy;
    [SerializeField] private GameObject mirror;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float castRadius = .3f;
    [SerializeField] private GameObject door;
    private Ray _ray;
    private float _lookTimer;
    private bool _inMirrorTableArea;
    private bool _isGettingMirror;

    // Дополнительные Поля P.S. Влад
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _morror;

    private void Update()
    {
        if (_isGettingMirror)
            return;

        if (!_inMirrorTableArea)
            return;

        Vector3 rayPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        Ray ray = Camera.main.ScreenPointToRay(rayPosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        RaycastHit hit;
        if (!Physics.SphereCast(ray.origin, castRadius, ray.direction, out hit)) return;

        if (!hit.transform.CompareTag("Mirror"))
        {
            _lookTimer = 0;
            return;
        }
            
        _lookTimer += Time.deltaTime;
        if (_lookTimer >= lookTime)
        {
            StartCoroutine(MirrorEvent());
        }
    }

    private void CloseDoor()
    {
        door.TryGetComponent(out Animator animator);
        animator.enabled = false;
        door.transform.localEulerAngles = Vector3.zero;

        //Код добавленный Владом
        _audioSource.Play();
    }

    private IEnumerator MirrorEvent()
    {
        _isGettingMirror = true;
        mirror.SetActive(true);
        CloseDoor();
        yield return new WaitForSeconds(timeBeforeSpawnEnemy);
        enemy.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inMirrorTableArea = true;
            //Код добавленный Владом
            Destroy(_morror.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _inMirrorTableArea = false;
    }
}