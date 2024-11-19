using Bell;
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
    private BellSoundTrigger _soundTrigger;

    private IStateSwitcher _switcher;
    private PlayerInput _playerInput;
    
    public NavMeshAgent Agent => _agent;
    public EnemyConfig Config => _config;
    public Transform[] Points => _points;
    public EnemyFieldOfView FOV => _fov;
    public BellSoundTrigger SoundTrigger => _soundTrigger;
    public Transform LastSoundPosition;

    [Inject]
    private void Construct(BellSoundTrigger trigger)
    {
        _soundTrigger = trigger;
    }
    
    private void Awake()
    {
        _switcher = new EnemyStateMachine(this, _fov, _view);
    }

    private void OnEnable()
    {
        SoundTrigger.OnBellSoundTriggered += LoudSound;
    }

    private void OnDisable()
    {
        SoundTrigger.OnBellSoundTriggered -= LoudSound;
    }
    
    private void LoudSound(Transform t)
    {
        LastSoundPosition = t;
    }

    private void Update()
    {
        _switcher.Update();
    }
}
