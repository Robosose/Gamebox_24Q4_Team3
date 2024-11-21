using Enemys.State;

namespace Enemys.StateMachine.States
{
    public class TutorPatrollingState : IState
    {
        private Enemy _enemy;
        private EnemyFieldOfView _fov;
        private IStateSwitcher _stateSwitcher;
        private EnemyView _enemyView;
        private int _currentPoint = 0;
        
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
            _stateSwitcher.SwitchState<TutorAttackState>();
        }

        public void Exit()
        {
            _fov.SeePlayer += OnSeePlayer;
            _enemyView.StopWalking();
        }

        public void Update()
        {
            if (_enemy.Agent.remainingDistance < .1f)
            {
                _currentPoint++;
                _stateSwitcher.SwitchState<TutorIdlingState>();
            }
        }
        
        private void SetDestination()
        {
            _enemy.Agent.SetDestination(_enemy.Points[_currentPoint].position);
        }
    }
}