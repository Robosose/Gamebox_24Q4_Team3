using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ReturnCursorStartScene : MonoBehaviour
{
    [SerializeField] private Image disclaimer;
    [SerializeField] private VideoPlayer player;
    private string _mainMenuKey = "MainMenu";
    
    private IEnumerator Start()
    {
        Sound.Instance.MuteMusicAndSound();
        
        Cursor.lockState = CursorLockMode.Locked;
        
        yield return new WaitForSeconds(7f);

        player.gameObject.SetActive(false);
        
        disclaimer.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(10f);

        disclaimer.gameObject.SetActive(false);
        
        Sound.Instance.UnmuteMusicAndSound();
        
        Cursor.lockState = CursorLockMode.None;
    }
}
