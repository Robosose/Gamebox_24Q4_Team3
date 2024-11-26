using Spawn;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SpawnInstaller:MonoInstaller
    {
        [SerializeField] private SpawnManager _spawnManager;

        public override void InstallBindings()
        {
            Container.Bind<SpawnManager>().FromInstance(_spawnManager).AsSingle().NonLazy();
        }
    }
}