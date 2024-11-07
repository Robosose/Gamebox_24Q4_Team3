using System;
using UnityEngine;

namespace Configs.Enemy
{
    [Serializable]
    public class AttackConfig
    {
        [Range(3,10)]public float Speed;
    }
}