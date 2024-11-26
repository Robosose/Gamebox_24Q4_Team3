using Enemys.State;
using UnityEngine;

namespace Enemys.StateMachine.States
{
    public class TutorPatrollingState : IState
    {
        private Enemy _enemy;
        private EnemyFieldOfView _fov;
        private IStateSwitcher _stateSwitcher;
        private EnemyView _enemyView;
        private int _currentPoint = 0;
        private bool _beIdling;
        
        public TutorPatrollingState(Enemy enemy, EnemyFieldOfView fov, IStateSwitcher stateSwitcher,
            EnemyView enemyView)
        {
            _enemy = enemy;
            _fov = fov;
            _stateSwitcher = stateSwitcher;
            _enemyView = enemyView;
        }

        public void Enter()
        {
            _enemy.Agent.speed = _enemy.Config.PatrolingConfig.Speed;
            SetDestination();
            _fov.SeePlayer += OnSeePlayer;
            _enemyView.StartWalking();
        }

        private void OnSeePlayer()
        {
            _stateSwitcher.SwitchState<TutorAttackState>();
        }

        public void Exit()
        {
            _fov.SeePlayer += OnSeePlayer;
            _enemyView.StopWalking();
            _currentPoint++;
        }

        public void Update()
        {
            if (_enemy.Agent.remainingDistance < .1f && !_beIdling)
            {
                _beIdling = true;
                _stateSwitcher.SwitchState<TutorIdlingState>();
            }
        }
        
        private void SetDestination()
        {
            _enemy.Agent.SetDestination(_enemy.Points[_currentPoint].position);
        }
    }
}