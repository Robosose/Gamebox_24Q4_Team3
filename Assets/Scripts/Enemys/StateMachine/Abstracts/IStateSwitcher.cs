namespace Enemys.State
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : IState;
        void Update();
    }
}