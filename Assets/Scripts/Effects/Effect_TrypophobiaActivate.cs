using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

public class Effect_TrypophobiaActivate : MonoBehaviour
{
    [SerializeField] private float _timeBeforeTrypophobia;
    [SerializeField] private float _trypophobiaShowSpeed;
    [SerializeField] private DecalProjector _decalProjector;
    
    private InputManager _inputManager;
    private float _waitTimer;
    private Material _decalMaterial;
    private float _tripophobiaValue;
    
    [Inject]
    private void Construct(InputManager inputManager)
    {
        _inputManager = inputManager;
    }

    private void Start()
    {
        _decalMaterial = _decalProjector.material;
    }

    private void Update()
    {
        if (!PlayerIsActive())
            _waitTimer += Time.deltaTime;
        else
            _waitTimer = 0;

        if (_waitTimer > _timeBeforeTrypophobia)
            _tripophobiaValue = Mathf.Clamp01(_tripophobiaValue + Time.deltaTime / _trypophobiaShowSpeed);
        else
            _tripophobiaValue = 0;
        
        _decalMaterial.SetFloat("_DissolveValue", _tripophobiaValue);
    }

    private bool PlayerIsActive()
    {
        return _inputManager.GetPlayerMovement().magnitude > 0.1f;
    }
}
