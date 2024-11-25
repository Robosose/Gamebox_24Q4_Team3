using System;
using UnityEngine;

namespace Configs.Enemy
{
    [Serializable]
    public class PatrolingConfig
    {
        [Range(0f, 5f)] public float Speed;
        [Range(1, 20)] public int IdlingTime;
    }
}