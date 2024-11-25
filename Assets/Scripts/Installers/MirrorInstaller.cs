using UnityEngine;
using Zenject;

public class MirrorInstaller : MonoInstaller
{
    [SerializeField] private MirrorController _mirrorController;

    public override void InstallBindings()
    {
        Container.Bind<MirrorController>().FromInstance(_mirrorController).AsSingle().NonLazy();
    }
}
