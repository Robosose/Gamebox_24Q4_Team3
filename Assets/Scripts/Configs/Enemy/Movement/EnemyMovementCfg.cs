using UnityEngine;

namespace Configs.Enemy.Movement
{
    [CreateAssetMenu(fileName = "EnemyMovementCFG", menuName = "Configs/Enemy/Movement")]
    public class EnemyMovementCfg : ScriptableObject
    {
        [field: SerializeField] public float Speed;
        [field: SerializeField] public int IdlingTimer;
        
    }
}