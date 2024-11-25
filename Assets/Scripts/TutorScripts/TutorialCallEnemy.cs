using System.Collections;
using UnityEngine;

public class TutorialCallEnemy : MonoBehaviour
{
    [SerializeField] private float lookTime;
    [SerializeField] private float timeBeforeSpawnEnemy;
    [SerializeField] private GameObject mirror;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float castRadius = .3f;

    private Ray _ray;
    private float _lookTimer;
    private bool _inMirrorTableArea;
    private bool _isGettingMirror;

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
        
        if (!hit.transform.CompareTag("Mirror")) return;
            
        _lookTimer += Time.deltaTime;
        if (_lookTimer >= lookTime)
        {
            StartCoroutine(MirrorEvent());
        }
    }
    
    //closeDoor
    

    private IEnumerator MirrorEvent()
    {
        _isGettingMirror = true;
        mirror.SetActive(true);
        yield return new WaitForSeconds(timeBeforeSpawnEnemy);
        enemy.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _inMirrorTableArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _inMirrorTableArea = false;
    }
}