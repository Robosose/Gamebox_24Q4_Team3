using UnityEngine;

namespace Configs.Enemy
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Enemy")]
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