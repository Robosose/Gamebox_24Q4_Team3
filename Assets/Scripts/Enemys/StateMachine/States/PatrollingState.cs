using System.Collections;
using Configs.Enemy;
using Enemys.State;
using UnityEngine;
using UnityEngine.AI;

namespace Enemys.StateMachine.States
{
    public class PatrollingState : IState
    {
        private Enemy _enemy;
        private PatrolingConfig _config;
        private IStateSwitcher _stateSwitcher;
        private NavMeshAgent _agent;
        private EnemyFieldOfView _fov;
        private int _currentPointIndex = 0;
        private bool _isIdling;
        private Coroutine _cor;
        
        public PatrollingState(Enemy enemy, PatrolingConfig cfg, EnemyFieldOfView fov, IStateSwitcher stateSwitcher)
        {
            _enemy = enemy;
            _config = cfg;
            _stateSwitcher = stateSwitcher;
            _agent = enemy.Agent;
            _fov = fov;
        }
        
        public void Enter()
        {
            _agent.speed = _config.Speed;
            _agent.SetDestination(_enemy.Points[_currentPointIndex].position);
        }
        
        public void Exit()
        {
            if(_cor is not null)
                _enemy.StopCoroutine(_cor);
            _isIdling = false;
        }

        public void Update()
        {
            if(_fov.IsSeePlayer)
                _stateSwitcher.SwitchState<AttackState>();
            
            if(_agent.remainingDistance >= 1f || _isIdling)
            {
                _isIdling = true;
                if(_cor is null)
                    _cor = _enemy.StartCoroutine(IdlingTimer());
                return;
            }
            if (_currentPointIndex >= _enemy.Points.Length - 1)
                _currentPointIndex = 0;
            else
                _currentPointIndex++;
            _agent.SetDestination(_enemy.Points[_currentPointIndex].position);
        }
        
        private IEnumerator IdlingTimer()
        {
            yield return new WaitForSeconds(_config.IdlingTime);
            _isIdling = false;
            _cor = null;
        }
    }
}