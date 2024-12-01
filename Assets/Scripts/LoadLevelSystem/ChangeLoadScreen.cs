using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;

public class ChangeLoadScreen : MonoBehaviour
{
    [SerializeField] private List<LoadImageByIndex> loadImagesByIndex;
    [SerializeField] private LocalizeSpriteEvent spriteEvent;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var data =  loadImagesByIndex.FirstOrDefault(i => i.index == SceneManager.GetActiveScene().buildIndex);
        spriteEvent.AssetReference.TableEntryReference = data.screenName;
    }
}
