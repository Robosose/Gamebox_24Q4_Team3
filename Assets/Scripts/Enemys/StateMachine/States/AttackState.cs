using Configs.Enemy;
using Enemys.State;
using TMPro;
using UnityEngine;

namespace Enemys.StateMachine.States
{
    public class AttackState : IState
    {
        private Enemy _enemy;
        private AttackConfig _config;
        private IStateSwitcher _stateSwitcher;
        private Transform _player;
        private EnemyView _view;

        
        public AttackState(Enemy enemy, AttackConfig configAttackConfig, IStateSwitcher stateSwitcher,
            EnemyView enemyView)
        {
            _enemy = enemy;
            _stateSwitcher = stateSwitcher;
            _config = configAttackConfig;
            _view = enemyView;
        }
        
        public void Enter()
        {
            Debug.Log(GetType());
            _enemy.Agent.speed = _config.Speed;
            _player = _enemy.FOV.PlayerRef.transform;
            _enemy.Agent.SetDestination(_player.position);
            _view.StartRunning();
        }

        public void Exit()
        {
            _view.StopRunning();
        }

        public void Update()
        {
            if (Vector3.Distance(_player.position, _enemy.Agent.destination) > 1)
            {
                _enemy.Agent.SetDestination(_player.position);
            }
        }
    }
}