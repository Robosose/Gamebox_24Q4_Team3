using System;
using System.Collections;
using Configs.Enemy;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemys
{
    public class EnemyFieldOfView : MonoBehaviour
    {
        [SerializeField] private Enemy enemy;
        
        
        [SerializeField] private bool _isSeePlayer;
        
        public bool IsSeePlayer => _isSeePlayer;
        public GameObject PlayerRef;
        private PatrollingDataConfig _Config;
        public EnemyConfig Config;
        
        private void Awake()
        {
            _Config = enemy.Config.PatrollingDataConfig;
            Config = enemy.Config;
            StartCoroutine(FOVRoutine());
            PlayerRef = GameObject.FindGameObjectWithTag("Player");
        }

        private IEnumerator FOVRoutine()
        {
            var wait = new WaitForSeconds(0.2f);
            
            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }

        private void FieldOfViewCheck()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _Config.Radius, _Config.TargetMask);
            
            if(rangeChecks.Length == 0)
            {
                _isSeePlayer = false;
                return;
            }
            
            var target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _Config.AngleOfView / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _Config.ObstructionMask))
                    _isSeePlayer = true;
                else
                    _isSeePlayer = false;
            }
            else
                _isSeePlayer = false;
        }
    }
}