using UnityEngine;

public class MenuButtonEvents : MonoBehaviour
{
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
