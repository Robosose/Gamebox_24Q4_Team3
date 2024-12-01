using System.Collections;
using Enums;
using UnityEngine;

namespace Player
{
    public class Voice : MonoBehaviour
    {
        [SerializeField] private AudioSource ohBellSource;
        [SerializeField] private AudioSource needHide;
        [SerializeField] private AudioSource whatHappening;
        [SerializeField] private AudioSource cryBaby;
        
        private bool _isTalking;

        public void ChosePhrase(PhrasesType phrasesType)
        {
            if(!_isTalking)
                StartCoroutine(PlayVoice(phrasesType));
        }

        private IEnumerator PlayVoice(PhrasesType phrasesType)
        {
            _isTalking = true;
            AudioSource source = null;
            switch (phrasesType)
            {
                case PhrasesType.OhBell:
                    source = ohBellSource;
                    break;
                case PhrasesType.HereAgain:
                    source = needHide;
                    break;
                case PhrasesType.WhatHappening:
                    source = whatHappening;
                    break;
                case PhrasesType.CryBaby:
                    source = cryBaby;
                    break;
            }

            source.Play();

            while (source.isPlaying)
            {
                yield return null;
            }

            _isTalking = false;
        }
    }
}