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
        private float _footstepTimer;
  
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
            SetDestination();
            _fov.SeePlayer += OnSeePlayer;
            _enemyView.StartWalking();
        }

        private void OnSeePlayer()
        {
            _stateSwitcher.SwitchState<AttackState>();
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

            FootstepTimer();
        }

        private void FootstepTimer()
        {
            _footstepTimer += Time.deltaTime;
            if (_footstepTimer >= _enemyView.FootstepIntervalWalk)
            {
                _enemyView.PlayRandomFootstep();
                _footstepTimer = 0f;
            }
        }

        private void SetDestination()
        {
            _enemy.Agent.SetDestination(_enemy.Points[_currentPoint].position);
        }
    }
}