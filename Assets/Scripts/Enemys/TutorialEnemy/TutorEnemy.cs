using Bell;
using Configs.Enemy;
using Enemys;
using Enemys.State;
using Enemys.StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class TutorEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyConfig _config;
    [SerializeField] private Transform[] _points;
    [SerializeField] private EnemyFieldOfView _fov;
    [SerializeField] private EnemyView _view;
    
    private BellSoundTrigger _soundTrigger;

    private IStateSwitcher _switcher;
    private PlayerInput _playerInput;
    
    public NavMeshAgent Agent => _agent;
    public EnemyConfig Config => _config;
    public Transform[] Points => _points;
    public EnemyFieldOfView FOV => _fov;
    
    
    private void Awake()
    {
        _switcher = new TutorEnemyStateMachine(this, _fov, _view);
    }

    private void Update()
    {
        _switcher.Update();
    }
}
