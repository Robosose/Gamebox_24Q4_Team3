using Configs.Enemy;
using Enemys.State;

namespace Enemys.StateMachine.States
{
    public class AttackState : IState
    {
        public AttackState(Enemy enemy, AttackConfig configAttackConfig, IStateSwitcher stateSwitcher)
        {
            
        }
        
        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}