using UnityEngine;

namespace Door
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] private DoorTrigger _doorTrigger;
        [SerializeField] private MeshRenderer _renderer;

        [SerializeField] private bool _isDoorOpen;

        private void OnEnable()
        {
            _doorTrigger.onDoorTriggerEnter += SwitchDoorState;
        }
        
        private void OnDisable()
        {
            _doorTrigger.onDoorTriggerEnter -= SwitchDoorState;
        }
        
        private void SwitchDoorState()
        {
            if (!_isDoorOpen)
                _renderer.enabled = false;
            else
                _renderer.enabled = true;
            
            _isDoorOpen = !_isDoorOpen;
        }
    }
}
