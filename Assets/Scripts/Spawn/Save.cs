using Spawn;
using UnityEngine;
using Zenject;

public class Save : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    
    // private SpawnManager _spawnManager;
    //
    // [Inject]
    // private void Construct(SpawnManager spawnManager)
    // {
    //     _spawnManager = spawnManager;
    // }
    //
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (!other.CompareTag("Player"))
    //         return;
    //
    //     _spawnManager.SetSpawnPoint(_spawnPoint.position);
    //     gameObject.SetActive(false);
    // }
}
