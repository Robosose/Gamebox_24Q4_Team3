using UnityEngine;
using UnityEngine.Serialization;

namespace Configs.Enemy
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Enemy", order = 0)]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private PatrollingDataConfig patrollingDataConfig;
        [SerializeField] private AttackConfig _attackConfig;
        [SerializeField] private PatrolingConfig patrolingConfig;
        
        public PatrollingDataConfig PatrollingDataConfig => patrollingDataConfig;
        public PatrolingConfig PatrolingConfig => patrolingConfig;
        public AttackConfig AttackConfig => _attackConfig;
    }
}