using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonEvents : MonoBehaviour
{
    public void StartGameButton()
    {
        SceneManager.LoadScene("Scenes/Mechanics/CubeScene");
    }
    
    public void ExitButton()
    {
        Application.Quit();
    }
}
