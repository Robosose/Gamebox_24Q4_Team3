using System.Collections;
using Enums;
using UnityEngine;

namespace Player
{
    public class Voice : MonoBehaviour
    {
        [SerializeField] private AudioSource voiceSource;
        [SerializeField] private AudioClip closeDoorClip;
        [SerializeField] private AudioClip ohBellClip;
        [SerializeField] private AudioClip hereAgainClip;
        [SerializeField] private AudioClip babyCryClip;
        [SerializeField] private AudioClip inKitchenClip;
        [SerializeField] private AudioClip openDoorClip;
        [SerializeField] private AudioClip whatHappening;
        [SerializeField] private bool tutor;
        private bool _isTalking;

        public void ChosePhrase(PhrasesType phrasesType)
        {
            if (phrasesType == PhrasesType.OhBell &&tutor)
                return;
            if(!_isTalking)
                StartCoroutine(PlayVoice(phrasesType));
        }

        private IEnumerator PlayVoice(PhrasesType phrasesType)
        {
            _isTalking = true;
            AudioClip clip = null;
            switch (phrasesType)
            {
                case PhrasesType.CloseDoor:
                    clip = closeDoorClip;
                    break;
                case PhrasesType.OhBell:
                    clip = ohBellClip;
                    break;
                case PhrasesType.HereAgain:
                    clip = hereAgainClip;
                    break;
                case PhrasesType.BabyCry:
                    clip = babyCryClip;
                    break;
                case PhrasesType.InKitchen:
                    clip = inKitchenClip;
                    break;
                case PhrasesType.OpenDoor:
                    clip = openDoorClip;
                    break;
                case PhrasesType.WhatHappening:
                    clip = whatHappening;
                    break;
            }

            voiceSource.clip = clip;
            voiceSource.Play();

            while (voiceSource.isPlaying)
            {
                yield return null;
            }

            _isTalking = false;
        }
    }
}