using System.Collections;
using System.Collections.Generic;
using Enemys.State;
using UnityEngine;

namespace Enemys.StateMachine.States
{
    public class TutorIdlingState : IState
    {
        private Enemy _enemy;
        private EnemyFieldOfView _fov;
        private IStateSwitcher _stateSwitcher;
        private EnemyView _enemyView;
        private Transform _lookAt;
        private Coroutine _cor;
        
        public TutorIdlingState(Enemy enemy, EnemyFieldOfView fov, IStateSwitcher stateSwitcher,
            EnemyView enemyView, Transform lookAt)
        {
            _enemy = enemy;
            _fov = fov;
            _stateSwitcher = stateSwitcher;
            _enemyView = enemyView;
            _lookAt = lookAt;
        }

        public void Enter()
        {
            _enemy.SeeEnemy += PlayerSeeEnemy;
            _fov.SeePlayer += SeePlayer;
            _enemy.Agent.isStopped = true;
            _enemy.transform.LookAt(_lookAt);
        }

        public void Exit()
        {
            _enemy.SeeEnemy -= PlayerSeeEnemy;
            _fov.SeePlayer -= SeePlayer;
            _enemy.Agent.isStopped = false;
            if(_cor != null)
                _enemy.StopCoroutine(_cor);
            _cor = null;
        }

        public void Update()
        {
            
        }

        private void SeePlayer()
        {
            _stateSwitcher.SwitchState<AttackState>();
        }

        private void PlayerSeeEnemy()
        {
            _cor = _enemy.StartCoroutine(TimeToSwitchState());
        }

        private IEnumerator TimeToSwitchState()
        {
            yield return new WaitForSeconds(2);
            _stateSwitcher.SwitchState<TutorPatrollingState>();

        }
    }
}