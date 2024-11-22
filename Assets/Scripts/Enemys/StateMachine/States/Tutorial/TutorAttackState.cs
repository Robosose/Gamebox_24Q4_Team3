using Configs.Enemy;
using Enemys.State;

namespace Enemys.StateMachine
{
    public class TutorAttackState : IState
    {
        private readonly Enemy _enemy;
        private readonly AttackConfig _configAttackConfig;
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly EnemyView _view;

        public TutorAttackState(Enemy enemy, AttackConfig configAttackConfig, EnemyStateMachine enemyStateMachine, EnemyView view)
        {
            _enemy = enemy;
            _configAttackConfig = configAttackConfig;
            _enemyStateMachine = enemyStateMachine;
            _view = view;
        }

        public void Enter()
        {
            _enemy.Agent.isStopped = false;
            _enemy.Agent.speed = _configAttackConfig.Speed;
            _view.StartRunning();
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            _enemy.Agent.SetDestination(_enemy.FOV.PlayerRef.transform.position);
        }
    }
}