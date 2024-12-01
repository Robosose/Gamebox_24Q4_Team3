using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private InputManager _inputManager;

    public override void InstallBindings()
    {
        Container.Bind<InputManager>().FromInstance(_inputManager).AsSingle().NonLazy();
    }
}