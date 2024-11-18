using UnityEngine;
using Zenject;

public class MirrorInstaller : MonoInstaller
{
    [SerializeField] private PlayerMirrorHandler _playerMirrorHandler;

    public override void InstallBindings()
    {
        Container.Bind<PlayerMirrorHandler>().FromInstance(_playerMirrorHandler).AsSingle().NonLazy();
    }
}
