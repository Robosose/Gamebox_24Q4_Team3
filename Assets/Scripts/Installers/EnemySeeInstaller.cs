using UnityEngine;
using Zenject;

namespace Installers
{
    public class EnemySeeInstaller:MonoInstaller
    {
        [SerializeField] private EnemySeeTrigger enemySeeTrigger;
        
        public override void InstallBindings()
        {
            Container.Bind<EnemySeeTrigger>().FromInstance(enemySeeTrigger).AsSingle().NonLazy();
        }
    }
}