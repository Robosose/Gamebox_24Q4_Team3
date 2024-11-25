using Patterns.States;
using UnityEngine;

namespace Mirror
{
    public class Mirror_States:MonoBehaviour 
    {
        [SerializeField] private Mirror_Rotate _rotateState;
        [SerializeField] private Mirror_Movement _movementState;

        private StateMachine _mirrorState;

        private void Start()
        {
            _mirrorState = new StateMachine();
            _mirrorState.ChangeState(_rotateState);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _mirrorState.ChangeState(_rotateState);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _mirrorState.ChangeState(_movementState);
            }
            _mirrorState.Update();
        }
    }
}