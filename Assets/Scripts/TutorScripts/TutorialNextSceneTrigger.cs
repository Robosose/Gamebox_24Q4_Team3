using UnityEngine;

public class TutorialNextSceneTrigger : MonoBehaviour
{
    [SerializeField] private MirrorTutorialEvent _mirrorTutorialEvent;
    [SerializeField] private GameObject _nextSceneTrigger;
    private void OnEnable()
    {
        _mirrorTutorialEvent.HitEnemy += EnableNextScene;
    }

    private void OnDisable()
    {
        _mirrorTutorialEvent.HitEnemy -= EnableNextScene;
    }

    private void EnableNextScene()
    {
        _nextSceneTrigger.SetActive(true);
    }
}
