using System;
using UnityEngine;

namespace Configs.Enemy
{
    [Serializable]
    public class PatrollingDataConfig
    {
        [Range(5, 100)] public float Radius;
        [Range(45, 180)] public float AngleOfView;
        public LayerMask TargetMask;
        public LayerMask ObstructionMask;
    }
}