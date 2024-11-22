using Enemys.State;

namespace Enemys.StateMachine.States
{
    public class TutorIdlingState : IState
    {
        private Enemy _enemy;
        private EnemyFieldOfView _fov;
        private IStateSwitcher _stateSwitcher;
        private EnemyView _enemyView;

        public TutorIdlingState(Enemy enemy, EnemyFieldOfView fov, IStateSwitcher stateSwitcher,
            EnemyView enemyView)
        {
            _enemy = enemy;
            _fov = fov;
            _stateSwitcher = stateSwitcher;
            _enemyView = enemyView;
        }

        public void Enter()
        {
            _enemy.SeeEnemy += PlayerSeeEnemy;
            _fov.SeePlayer += SeePlayer;
            _enemy.Agent.isStopped = true;
        }

        public void Exit()
        {
            _enemy.SeeEnemy -= PlayerSeeEnemy;
            _fov.SeePlayer -= SeePlayer;
            _enemy.Agent.isStopped = false;
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
            _stateSwitcher.SwitchState<TutorPatrollingState>();
        }
    }
}