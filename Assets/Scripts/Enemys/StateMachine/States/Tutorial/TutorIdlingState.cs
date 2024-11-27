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
        private float _voiceTimer;

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
        }

        public void Update()
        {
            VoiceTimer();
        }

        private void SeePlayer()
        {
            _stateSwitcher.SwitchState<AttackState>();
        }

        private void PlayerSeeEnemy()
        {
            _stateSwitcher.SwitchState<TutorPatrollingState>();
        }

        private void VoiceTimer()
        {
            _voiceTimer += Time.deltaTime;
            if (_voiceTimer >= _enemyView.MonsterVoicesInterval)
            {
                _enemyView.PlayMonsterVoices();
                _voiceTimer = 0f;
            }
        }
    }
}