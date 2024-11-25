using System;
using System.Collections.Generic;
using System.Linq;
using Enemys.State;
using Enemys.StateMachine.States;

namespace Enemys.StateMachine
{
    public class TutorEnemyStateMachine : IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;
        
        public TutorEnemyStateMachine(TutorEnemy enemy, EnemyFieldOfView fov, EnemyView view)
        {
            // _states = new List<IState>()
            // {
            //     new TutorPatrollingState(enemy, fov, this, view),
            //     new TutorialIdlingState(enemy, fov, this, view, enemy.Mirror),
            //     new TutorAttackState(enemy, enemy.Config.AttackConfig, this, view),
            // };
            
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