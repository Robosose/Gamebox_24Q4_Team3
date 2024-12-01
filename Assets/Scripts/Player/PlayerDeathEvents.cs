using UnityEngine;

public class PlayerDeathEvents : MonoBehaviour
{
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void Restart()
    {
        SceneSaver.Instance.LoadScene(ScenesType.CurrentScene);
    }

    public void MainMenu()
    {
        SceneSaver.Instance.LoadScene(ScenesType.MainMenuScene);
    }
}