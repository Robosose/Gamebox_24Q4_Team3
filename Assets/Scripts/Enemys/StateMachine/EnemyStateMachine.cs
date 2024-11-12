using System;
using System.Collections.Generic;
using System.Linq;
using Enemys.State;
using Enemys.StateMachine.States;

namespace Enemys.StateMachine
{
    public class EnemyStateMachine : IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;

        public EnemyStateMachine(Enemy enemy, EnemyFieldOfView fov)
        {
            _states = new List<IState>()
            {
                new PatrollingState(enemy, enemy.Config.PatrolingConfig, fov, this),
                new AttackState(enemy, enemy.Config.AttackConfig, this),
                new AgrOnSoundState(fov, enemy, enemy.Config, this)
            };
            
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
            _currentState.Update();
        }
    }
}