using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PlayerInput _playerInput;

    public override void InstallBindings()
    {
        Container.Bind<InputManager>().FromInstance(_inputManager).AsSingle().NonLazy();
        Container.Bind<PlayerInput>().FromInstance(_playerInput).AsSingle().NonLazy();
    }
}