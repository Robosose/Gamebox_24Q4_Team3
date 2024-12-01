using UnityEngine;

namespace Player
{
    public class SlowColliders:MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerControllers playerControllers))
            {
                playerControllers.SlowMove(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerControllers playerControllers))
            {
                playerControllers.SlowMove(false);
            }
        }
    }
}