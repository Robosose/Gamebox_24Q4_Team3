using System;
using UnityEngine;

public class MirrorTutorialEvent : MonoBehaviour
{
    [SerializeField] private bool _isTutor;
    [SerializeField] private LayerMask ignoreMask;
    public Action HitEnemy;
    private bool _hit;

    //Поля дабавленные Владом
    [SerializeField] private GameObject _triggerNextScene;

    private void Update()
    {
        if (_hit)
            return;
        
        if (!_isTutor)
            return;

        RaycastHit hit = new RaycastHit();
        int ignore = ~ignoreMask;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ignore))
        {
            if (!hit.transform.CompareTag("Enemy"))
                return;
            
            hit.transform.TryGetComponent(out Enemy enemy);
            if (enemy)
            {
                enemy.SeeEnemy?.Invoke();
                HitEnemy?.Invoke();
                _hit = true;           
            }

            //Код добавленный Владом
            if (_triggerNextScene is null)
                return;
            if(_triggerNextScene != null)
                _triggerNextScene.SetActive(true);
        }
    }
}
