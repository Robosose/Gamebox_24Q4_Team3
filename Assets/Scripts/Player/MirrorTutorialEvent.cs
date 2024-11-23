using UnityEngine;

public class MirrorTutorialEvent : MonoBehaviour
{
    [SerializeField] private bool _isTutor;
    [SerializeField] private LayerMask ignoreMask;
    private bool _hit;
    
    private void Update()
    {
        if(!_isTutor)
            return;
        RaycastHit hit;
        int layerMask = ~(1 << ignoreMask);
        if (Physics.Raycast(transform.position, transform.forward, out hit,Mathf.Infinity,layerMask))
        {
            print(hit.transform.tag);
            if(!hit.transform.CompareTag("Enemy"))
                return;
            if (!_hit)
            {
                hit.transform.TryGetComponent(out Enemy enemy);
                if (enemy)
                {
                    enemy.SeeEnemy?.Invoke();
                }
                _hit = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position,transform.forward * 50f);
    }
}
