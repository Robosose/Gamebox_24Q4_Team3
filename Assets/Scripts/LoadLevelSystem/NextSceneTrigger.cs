using UnityEngine;

public class NextSceneTrigger : MonoBehaviour
{
    [SerializeField] private int nextSceneIndex;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        SceneSaver.Instance.LoadSceneByIndex(nextSceneIndex);
    }
}
