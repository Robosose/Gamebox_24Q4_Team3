using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonEvents : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    
    private void Start()
    {
        continueButton.interactable = SceneSaver.Instance.CheckSave();
    }

    public void StartNewGameButton()
    {
        SceneSaver.Instance.LoadScene(ScenesType.StartScene);
    }

    public void ContinueButton()
    {
        SceneSaver.Instance.LoadScene(ScenesType.SavedScene);
    }
    
    public void ExitButton()
    {
        Application.Quit();
    }
}
