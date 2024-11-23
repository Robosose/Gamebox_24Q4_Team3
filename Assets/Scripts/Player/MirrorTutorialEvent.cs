using UnityEngine;

public class MirrorTutorialEvent : MonoBehaviour
{
    [SerializeField] private bool _isTutor;
    private bool _hit;
    
    private void Update()
    {
        if(!_isTutor)
            return;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit,  LayerMask.NameToLayer("Enemy")))
        {
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
