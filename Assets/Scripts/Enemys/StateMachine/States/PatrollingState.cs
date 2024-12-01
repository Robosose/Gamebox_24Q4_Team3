using System.Collections;
using Configs.Enemy;
using Enemys.State;
using UnityEngine;
using UnityEngine.AI;
using Zenject.SpaceFighter;

namespace Enemys.StateMachine.States
{
    public class PatrollingState : IState
    {
        private Enemy _enemy;
        private PatrolingConfig _config;
        private IStateSwitcher _stateSwitcher;
        private NavMeshAgent _agent;
        private EnemyFieldOfView _fov;
        private int _currentPointIndex;
        private bool _isIdling;
        private Coroutine _cor;
        private EnemyView _view;
        private bool _isRandomPatrolling;
        private float _footstepTimer;
        private float _voiceTimer;

        public PatrollingState(Enemy enemy, PatrolingConfig cfg, EnemyFieldOfView fov, IStateSwitcher stateSwitcher,
            EnemyView enemyView, bool isRandomPatrolling)
        {
            _enemy = enemy;
            _config = cfg;
            _stateSwitcher = stateSwitcher;
            _agent = enemy.Agent;
            _fov = fov;
            _view = enemyView;
            _isRandomPatrolling = isRandomPatrolling;
        }
        
        public void Enter()
        {
            _agent.speed = _config.Speed;
            _enemy.NewSoundPosition += OnLoudSound;
            _fov.SeePlayer += OnSeePlayer;
            _currentPointIndex = 0;
            _view.StartWalking();
            _agent.SetDestination(_enemy.Points[_currentPointIndex].position);          
        }

        private void OnSeePlayer()
        {
            _stateSwitcher.SwitchState<AttackState>();
        }

        private void OnLoudSound(Transform transform)
        {
            if(transform is null)
                return;
            if(_cor is not null)
                _enemy.StopCoroutine(_cor);
            _cor = null;
            _stateSwitcher.SwitchState<AgrOnSoundState>();
        }


        public void Exit()
        {
            if(_cor is not null)
                _enemy.StopCoroutine(_cor);
            _isIdling = false;
            _view.StopWalking();
            _fov.SeePlayer -= OnSeePlayer;
            _enemy.NewSoundPosition -= OnLoudSound;

        }

        public void Update()
        {
            if(_agent.remainingDistance <= .1f || _isIdling )
            {
                _isIdling = true;
                if(_cor is null)
                    _cor = _enemy.StartCoroutine(IdlingTimer());
                VoiceTimer();
                return;
            }

            FootstepTimer();
        }

        private void SetNextPoint()
        {
            if (_currentPointIndex >= _enemy.Points.Length - 1)
                _currentPointIndex = 0;
            else
                _currentPointIndex++;
        }

        private void SetRandomPoint()
        {
            var tmp = Random.Range(0, _enemy.Points.Length);
            if(tmp == _currentPointIndex || _enemy.Points[tmp] is null)
                SetRandomPoint();
            _currentPointIndex = tmp;
        }
        
        private IEnumerator IdlingTimer()
        {
            _view.StopWalking();
            yield return new WaitForSeconds(_config.IdlingTime);
            
            if (_isRandomPatrolling)
                SetRandomPoint();
            else
                SetNextPoint();
            
            _agent.SetDestination(_enemy.Points[_currentPointIndex].position);
            _view.StartWalking();
            _isIdling = false;
            _cor = null;
        }

        private void FootstepTimer()
        {
            _footstepTimer += Time.deltaTime;
            if (_footstepTimer >= _view.FootstepIntervalWalk)
            {
                _view.PlayRandomFootstep();
                _footstepTimer = 0f;
            }
        }

        private void VoiceTimer()
        {
            _voiceTimer += Time.deltaTime;
            if (_voiceTimer >= _view.MonsterVoicesInterval)
            {
                _view.PlayMonsterVoices();
                _voiceTimer = 0f;
            }
        }
    }
}