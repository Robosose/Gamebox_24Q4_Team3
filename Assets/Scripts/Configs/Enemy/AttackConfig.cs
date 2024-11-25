using System;
using UnityEngine;

namespace Configs.Enemy
{
    [Serializable]
    public class AttackConfig
    {
        [Range(.5f,10)]public float Speed;
    }
}