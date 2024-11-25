using UnityEngine;
using Zenject;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _menuPausePanel;
    [SerializeField] private GameObject _settingsPanel;
    private InputManager _inputManager;
    private bool _isPaused;
    
    [Inject]
    private void Construct(InputManager inputManager)
    {
        _inputManager = inputManager;
    }
    private void Update()
    {
        if (_inputManager.IsPause())
        {
            if (!_isPaused)
                Pause();
            else
                Resume();
        }
    }

    private void Pause()
    {
        _menuPausePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        _isPaused = true;
    }

    public void Resume()
    {
        if (_settingsPanel.activeSelf)
            _settingsPanel.SetActive(false);
        _menuPausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        _isPaused = false;
    }
}
