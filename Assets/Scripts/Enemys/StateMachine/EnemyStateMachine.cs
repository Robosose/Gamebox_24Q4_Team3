using System;
using System.Collections.Generic;
using System.Linq;
using Enemys.State;
using Enemys.StateMachine.States;
using UnityEngine;

namespace Enemys.StateMachine
{
    public class EnemyStateMachine : IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;

        public EnemyStateMachine(Enemy enemy, EnemyFieldOfView fov, EnemyView view, bool isTutor, bool isRandomPatroller)
        {
            _states = isTutor ? CreateTutorStates(enemy, fov, view) : CreateStandardStates(enemy, fov, view, isRandomPatroller);
            _currentState = _states[0];
            _currentState.Enter();
        }
        
        public void SwitchState<T>() where T : IState
        {
            _currentState.Exit();
            _currentState = _states.FirstOrDefault(state => state is T);
            if (_currentState is null)
                throw new ArgumentNullException($"{nameof(_currentState)} is null.");
            _currentState.Enter();
        }

        public void Update()
        {
            if(_currentState is null) 
                throw new ArgumentNullException($"{nameof(_currentState)} is null.");
            Debug.Log(_currentState.GetType());
            _currentState.Update();
        }

        private List<IState> CreateTutorStates(Enemy enemy, EnemyFieldOfView fov, EnemyView view) => new List<IState>()
        {
            new TutorPatrollingState(enemy, fov, this, view),
            new TutorIdlingState(enemy, fov, this, view, enemy.LookAt),
            new AttackState(enemy, enemy.Config.AttackConfig, this, view),
        };

        private List<IState> CreateStandardStates(Enemy enemy, EnemyFieldOfView fov, EnemyView view,
            bool isRandomPatroller) =>
            new List<IState>()
            {
                new PatrollingState(enemy, enemy.Config.PatrolingConfig, fov, this, view, isRandomPatroller),
                new AttackState(enemy, enemy.Config.AttackConfig, this, view),
                new AgrOnSoundState(fov, enemy, enemy.Config, this, view)
            };
    }
}