using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoiceAtTheStart : MonoBehaviour
{
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex == 3 || currentScene.buildIndex == 5)
        {
            var voiceComponent = GetComponent<Voice>();

            if (voiceComponent != null)
            {
                voiceComponent.ChosePhrase(Enums.PhrasesType.WhatHappening);
            }
            else
            {
                Debug.LogWarning("Voice null.");
            }
        }
    }
}