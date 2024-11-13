namespace Patterns.States
{
    public interface IMirrorState
    {
        void Enter();
        void Execute();
        void FixedExecute();
        void Exit();
    }
}