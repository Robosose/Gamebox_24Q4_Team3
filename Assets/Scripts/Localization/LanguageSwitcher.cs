using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    private string _currentLocal;

    public void ChoseLanguage()
    {
        if (_dropdown.value == 0)
        {
            ChangeLanguage("en");
        }

        if (_dropdown.value == 1)
        {
            ChangeLanguage("ru");
        }
    }

    private void ChangeLanguage(string localeCode)
    {
        var selectedLocale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);
        if (selectedLocale != null)
        {
            LocalizationSettings.SelectedLocale = selectedLocale;
        }
        else
        {
            Debug.LogWarning($"Locale '{localeCode}' not found.");
        }
    }
}
