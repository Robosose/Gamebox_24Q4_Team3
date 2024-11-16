using Configs.Enemy;
using Enemys;
using Enemys.State;
using Enemys.StateMachine;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyConfig _config;
    [SerializeField] private Transform[] _points;
    [SerializeField] private EnemyFieldOfView _fov;
    [SerializeField] private EnemyView _view;

    private IStateSwitcher _switcher;
    private PlayerInput _playerInput;
    
    public NavMeshAgent Agent => _agent;
    public EnemyConfig Config => _config;
    public Transform[] Points => _points;
    public EnemyFieldOfView FOV => _fov;
    public PlayerInput PlayerInput => _playerInput;
    
    
    [Inject]
    private void Construct(PlayerInput input)
    {
        _playerInput = input;
    }
    
    private void Awake()
    {
        _switcher = new EnemyStateMachine(this, _fov, _view);
    }

    private void Update()
    {
        _switcher.Update();
    }
}
