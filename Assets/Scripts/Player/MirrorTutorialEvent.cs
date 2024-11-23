using UnityEngine;

public class MirrorTutorialEvent : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position,Vector3.forward * 50f);
    }
}
