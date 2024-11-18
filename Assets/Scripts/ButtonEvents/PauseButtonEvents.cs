using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ButtonEvents
{
    public class PauseButtonEvents:MonoBehaviour
    {
        private PauseManager _pauseManager;
        
        [Inject]
        private void Construct(PauseManager pauseManager)
        {
            _pauseManager = pauseManager;
        }

        public void ResumeButton()
        {
            _pauseManager.Resume();
        }
        
        public void LoadMenuButton()
        {
            SceneSaver.Instance.LoadScene(ScenesType.MainMenuScene);
            Time.timeScale = 1;
        }

        public void RestartButton()
        {
            SceneSaver.Instance.LoadScene(ScenesType.CurrentScene);
            Time.timeScale = 1;
        }
    }
}