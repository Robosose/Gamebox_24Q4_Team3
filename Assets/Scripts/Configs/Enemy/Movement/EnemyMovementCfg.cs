using UnityEngine;

namespace Configs.Enemy.Movement
{
    public class EnemyMovementCfg : ScriptableObject
    {
        [field: SerializeField] public float Speed;
        [field: SerializeField] public int IdlingTimer;
        
    }
}