using Enums;
using Player;
using UnityEngine;

public class CharacterVoiceTrigger : MonoBehaviour
{
    [SerializeField] private PhrasesType phrase;
    [SerializeField] private bool disable;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Voice voice))
        {
            voice.ChosePhrase(phrase);
        }
        
        if (disable)
            gameObject.SetActive(false);
    }
}