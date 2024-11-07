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
        private int _currentPointIndex = 0;
        private bool _isIdling;
        private Coroutine _cor;
        
        public PatrollingState(Enemy enemy, PatrolingConfig cfg, EnemyFieldOfView fov, IStateSwitcher stateSwitcher)
        {
            _enemy = enemy;
            _config = cfg;
            _stateSwitcher = stateSwitcher;
            _agent = enemy.Agent;
        }
        
        public void Enter()
        {
            _agent.speed = _config.Speed;
            _agent.SetDestination(_enemy.Points[_currentPointIndex].position);
        }
        
        public void Exit()
        {
            _enemy.StopCoroutine(_cor);
            _isIdling = false;
        }

        public void Update()
        {
            if(_agent.remainingDistance >= 1f || _isIdling)
            {
                _isIdling = true;
                return;
            }
            _cor = _enemy.StartCoroutine(IdlingTimer());
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