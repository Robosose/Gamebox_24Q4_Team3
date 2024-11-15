using System.Collections;
using Configs.Enemy;
using Enemys.State;
using UnityEngine;

namespace Enemys.StateMachine.States
{
    public class AgrOnSoundState : IState
    {
        private EnemyFieldOfView _fov;
        private Enemy _enemy;
        private EnemyConfig _config;
        private IStateSwitcher _stateSwitcher;
        private Coroutine _coroutine;
        private EnemyView _view;

        public AgrOnSoundState(EnemyFieldOfView fov, Enemy enemy, EnemyConfig config,
            IStateSwitcher stateSwitcher, EnemyView enemyView)
        {
            _fov = fov;
            _enemy = enemy;
            _config = config;
            _stateSwitcher = stateSwitcher;
            _view = enemyView;
        }

        public void Enter()
        {
            Debug.Log(GetType());

            _enemy.Agent.speed = _config.AttackConfig.Speed;
            _enemy.PlayerInput.LoudSound += SetNewDestination;
            SetNewDestination();
            _view.StartRunning();
        }

        public void Exit()
        {
            ZeroingOutCoroutine();
            _enemy.PlayerInput.LoudSound -= SetNewDestination;
            _view.StopRunning();
        }

        public void Update()
        {
            if (_enemy.Agent.remainingDistance < 1f)
                _coroutine = _enemy.StartCoroutine(IdlingTimer());
            if (_fov.IsSeePlayer)
                _stateSwitcher.SwitchState<AttackState>();
        }

        private void ZeroingOutCoroutine()
        {
            if (_coroutine is not null)
                _enemy.StopCoroutine(_coroutine);
            _coroutine = null;
        }
        
        private void SetNewDestination()
        {
            ZeroingOutCoroutine();
            if(Vector3.Distance(_enemy.Agent.destination.normalized, _fov.PlayerRef.transform.position) < 1f)
                return;
            _enemy.Agent.SetDestination(_fov.PlayerRef.transform.position);
        }
        
        private IEnumerator IdlingTimer()
        {
            _view.StopRunning();
            yield return new WaitForSeconds(_config.PatrolingConfig.IdlingTime);
            _stateSwitcher.SwitchState<PatrollingState>();
            _coroutine = null;
        }
    }
}