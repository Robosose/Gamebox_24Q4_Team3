using System.Collections;
using Configs.Enemy.Movement;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
   [SerializeField] private EnemyMovementCfg _movementCfg;
   [SerializeField] private NavMeshAgent _agent;
   [SerializeField] private Transform[] _points;

   private Coroutine _cor;
   private int _currentPointIndex = 0;
   private bool _isIdling;
   
   private void Awake()
   {
      _agent.speed = _movementCfg.Speed;
      _agent.SetDestination(_points[0].position);
   }

   private void Update()
   {
      if(_agent.remainingDistance >= 1f || _isIdling)
         return;
      _cor = StartCoroutine(IdlingTimer());
      if (_currentPointIndex >= _points.Length - 1)
         _currentPointIndex = 0;
      else
         _currentPointIndex++;
      _agent.SetDestination(_points[_currentPointIndex].position);
   }

   private IEnumerator IdlingTimer()
   {
      _isIdling = true;
      yield return new WaitForSeconds(_movementCfg.IdlingTimer);
      _isIdling = false;
      _cor = null;
   }

   public void SetDestinationToSound(Transform soundTransform)
   {
      if(_cor is not null)
         StopCoroutine(_cor);
      _isIdling = false;
      _agent.SetDestination(soundTransform.position);
   }
}
