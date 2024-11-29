using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Configs.Enemy;
using UnityEngine;
using Zenject;

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
        public Action SeePlayer;

        private EnemySeeTrigger _enemySeeTrigger;

        [Inject]
        private void Construct(EnemySeeTrigger enemySeeTrigger)
        {
            _enemySeeTrigger = enemySeeTrigger;
        }
        
        private void Awake()
        {
            _Config = enemy.Config.PatrollingDataConfig;
            Config = enemy.Config;
            StartCoroutine(FOVRoutine());
        }

        private IEnumerator FOVRoutine()
        {
            var wait = new WaitForSeconds(0.1f);
            
            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }

        private void FieldOfViewCheck()
        {
            if (_isSeePlayer)
            {
                return;
            }
         
            List<Collider> rangeChecks = Physics.OverlapSphere(transform.position, _Config.Radius, _Config.TargetMask).ToList();
            if (rangeChecks.Count == 0)
            {
                _isSeePlayer = false;
                return;
            }

            rangeChecks.ForEach(t =>
            {
                var target = t.transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToTarget) < _Config.AngleOfView / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget,
                            _Config.ObstructionMask))
                    {
                        _isSeePlayer = true;
                        PlayerRef = target.gameObject;
                        SeePlayer?.Invoke();
                        _enemySeeTrigger.SeePlayer?.Invoke(true);
                        print($"See player at positions: {target.position}: {target.localPosition}");
                    }
                    else
                        _isSeePlayer = false;
                }
                else
                    _isSeePlayer = false;
            });
        }
    }
}