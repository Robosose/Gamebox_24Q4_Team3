using UnityEngine;
using Zenject;

public class PlayerMirrorHandler : MonoBehaviour
{
    private InputManager _inputManager;
    private MirrorController _itemController;

    [Inject]
    private void Construct(InputManager inputManager, MirrorController itemController)
    {
        _inputManager = inputManager;
        _itemController = itemController;
    }
}
