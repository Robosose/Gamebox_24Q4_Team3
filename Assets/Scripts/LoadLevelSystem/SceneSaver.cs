using Patterns.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSaver : Singleton<SceneSaver>
{
    [SerializeField] private Animator _loadAnimator;
    [SerializeField] private Image _background;
    [SerializeField] private GameObject _slider;
    private static readonly int Load = Animator.StringToHash("Load");
    private string _levelName;
    private const string LevelNameKey = "LevelName";
    private AsyncOperation _loadingAsyncOperation;

    private void Start()
    {
        _background.color = Color.clear;
        _slider.SetActive(false);
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _background.color = Color.clear;
        _slider.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex != 0)
            SaveCurrentScene();
    }

    private void OnSceneUnloaded(Scene scene)
    {
        _background.color = Color.clear;
        _slider.SetActive(false);
        _loadAnimator.ResetTrigger(Load);
    }

    private void SaveCurrentScene()
    {
        _levelName = SceneManager.GetActiveScene().name;
        ES3.Save(LevelNameKey, _levelName);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        _loadingAsyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        _loadingAsyncOperation.allowSceneActivation = false;
        ActivateAnimation();
    }

    public void LoadScene(ScenesType scenesType)
    {
        ActivateAnimation();
        switch (scenesType)
        {
            case ScenesType.MainMenuScene:
                _loadingAsyncOperation = SceneManager.LoadSceneAsync(0);
                break;
            case ScenesType.SavedScene:
                string savedSceneName = ES3.Load(LevelNameKey).ToString();
                _loadingAsyncOperation = SceneManager.LoadSceneAsync(savedSceneName);
                break;
            case ScenesType.StartScene:
                _loadingAsyncOperation = SceneManager.LoadSceneAsync(1);
                break;
            case ScenesType.CurrentScene:
                _loadingAsyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
                break;
            case ScenesType.NextScene:
                _loadingAsyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }

        if (_loadingAsyncOperation != null)
        {
            _loadingAsyncOperation.allowSceneActivation = false;
        }
    }

    private void ActivateAnimation()
    {
        Time.timeScale = 0;
        _background.color = Color.white;
        _slider.SetActive(true);
        _loadAnimator.SetTrigger(Load);
    }

    public bool CheckSave()
    {
        if (!ES3.FileExists())
            return false;
        return ES3.KeyExists(LevelNameKey);
    }

    public void OpenLoadedScene()
    {
        if (_loadingAsyncOperation != null)
        {
            Time.timeScale = 1;
            _loadingAsyncOperation.allowSceneActivation = true;
        }
    }
}