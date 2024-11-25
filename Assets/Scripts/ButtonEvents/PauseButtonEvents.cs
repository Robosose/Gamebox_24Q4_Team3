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
            SceneManager.LoadScene("Scenes/MainMenuScene/MainMenu");
            Time.timeScale = 1;
        }

        public void RestartButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }
    }
}