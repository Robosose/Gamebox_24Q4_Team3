namespace Patterns.States
{
    public class StateMachine
    {
        private IMirrorState _currentMirrorState;
    
        public void ChangeState(IMirrorState newMirrorState)
        {
            if(_currentMirrorState== newMirrorState)
                return;
            
            _currentMirrorState?.Exit();
            _currentMirrorState = newMirrorState;
            _currentMirrorState?.Enter();
        }

        public void Update()
        {
            _currentMirrorState?.Execute();
        }

        public void FixedUpdate()
        {
            _currentMirrorState?.FixedExecute();
        }
    }
}