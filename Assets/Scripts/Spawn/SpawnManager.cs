using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Spawn
{
    public class SpawnManager:MonoBehaviour
    {
        [SerializeField] private GameObject _characterPrefab;
        [SerializeField] private Transform _startSpawnPoint;
        [SerializeField] private CinemachineVirtualCameraBase _cinemachine;
        [SerializeField] private GameObject _cameraHandler;
        private DiContainer _container;
        private Vector3 _spawnPoint;
        private bool _newSpawn;
        
        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _container = diContainer;
        }
        
        private void Start()
        {
            SpawnCharacter();
        }

        private void SpawnCharacter()
        {
            var character = _container.InstantiatePrefab(_characterPrefab);
            character.transform.position = _newSpawn?_startSpawnPoint.position:_spawnPoint;
        }
        
        public void SetSpawnPoint(Vector3 spawnPoint)
        {
            _spawnPoint = spawnPoint;
            _newSpawn = true;
        }
    }
}