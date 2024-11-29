using UnityEngine;

public class SetLanguage : MonoBehaviour
{
    public void ChangeLocal()
    {
        LanguageSwitcher.Instance.ChangeLanguage();
    }
}
