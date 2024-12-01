using Enemys;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyFieldOfView))]
public class EnemyFieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        var fov = (EnemyFieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.Config.PatrollingDataConfig.Radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y,
            -fov.Config.PatrollingDataConfig.AngleOfView / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y,
            fov.Config.PatrollingDataConfig.AngleOfView / 2);
        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.Config.PatrollingDataConfig.Radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.Config.PatrollingDataConfig.Radius);

        if (fov.IsSeePlayer)
        {
            Handles.color = Color.red;
            Handles.DrawLine(fov.transform.position, fov.PlayerRef.transform.position);
        }
        else
        {
            Handles.color = Color.blue;
            Handles.DrawLine(fov.transform.position, fov.transform.forward * 90);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
