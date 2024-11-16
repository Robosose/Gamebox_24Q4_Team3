using UnityEngine;
using Zenject;

namespace Installers
{
    public class PauseInstaller:MonoInstaller
    {
        [SerializeField] private PauseManager _pauseManager;

        public override void InstallBindings()
        {
            Container.Bind<PauseManager>().FromInstance(_pauseManager).AsSingle().NonLazy();
        }
    }
}