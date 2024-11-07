using Configs.Enemy;
using Enemys;
using Enemys.State;
using Enemys.StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyConfig _config;
    [SerializeField] private Transform[] _points;
    
    private IStateSwitcher _switcher;
    private EnemyFieldOfView _fov;
    public NavMeshAgent Agent => _agent;
    public EnemyConfig Config => _config;
    public Transform[] Points => _points;
    public EnemyFieldOfView FOV => _fov;
    
    private void Awake()
    {
        _switcher = new EnemyStateMachine(this, _fov);
        Agent.isStopped = true;
    }

    private void Update()
    {
        _switcher.Update();
    }
}
