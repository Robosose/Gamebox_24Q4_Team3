using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Animator deathScreenAnimator;
    [SerializeField] private float smoothTime = 0.5f;
    [SerializeField] private float thresholdAngle = 1f;
    
    private bool _isAlign;
    public Action OnAligned;
    private static readonly int Death = Animator.StringToHash("Death");

    public void IsDead(Vector3 target)
    {
        print(_isAlign);
        if(!_isAlign)
            StartCoroutine(Dead(target));
    }

    private IEnumerator Dead(Vector3 target)
    {
        _isAlign = true;
        
        Vector3 directionToTarget = target - transform.position;

        directionToTarget.y = 0;

        Vector3 forward = transform.forward;
        Vector3 targetDirection = directionToTarget.normalized;
            
        float angle;
        
        angle  = Vector3.Angle(forward, targetDirection);

        while (angle > thresholdAngle)
        {
            directionToTarget = target - transform.position;

            directionToTarget.y = 0;

            forward = transform.forward;
            targetDirection = directionToTarget.normalized;
            
            angle  = Vector3.Angle(forward, targetDirection);
            
            if (directionToTarget.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

                Quaternion currentRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                Quaternion adjustedTargetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

                transform.rotation = Quaternion.Slerp(
                    currentRotation,
                    adjustedTargetRotation,
                    Time.deltaTime / smoothTime
                );
            }

            yield return null;
        }

        deathScreenAnimator.SetTrigger(Death);
        OnAligned?.Invoke();
        Time.timeScale = 0;
    }
}