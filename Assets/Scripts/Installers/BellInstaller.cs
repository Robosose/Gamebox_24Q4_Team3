using Bell;
using UnityEngine;
using Zenject;

public class BellInstaller : MonoInstaller
{
    [SerializeField] private BellSoundTrigger _trigger;
    
    public override void InstallBindings()
    {
        Container.Bind<BellSoundTrigger>()
            .FromInstance(_trigger)
            .AsSingle()
            .NonLazy();
    }
}