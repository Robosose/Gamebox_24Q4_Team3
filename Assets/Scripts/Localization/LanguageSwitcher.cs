using Patterns.Singleton;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSwitcher : Singleton<LanguageSwitcher>
{
    private string _enLocal = "en";
    private string _ruLocal = "ru";
    private string _currentLocal;
    private const string DropdownValue = "DropdownValue";

    private void Start()
    {
        _currentLocal = ES3.Load(DropdownValue).ToString();
        ChangeLanguage(_currentLocal);
    }
    
    public void ChangeLanguage(string localeCode)
    {
        _currentLocal = localeCode;
        var selectedLocale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);
        if (selectedLocale != null)
        {
            LocalizationSettings.SelectedLocale = selectedLocale;
            ES3.Save(DropdownValue,_currentLocal);
        }
        else
        {
            Debug.LogWarning($"Locale '{localeCode}' not found.");
        }
    }

    private void OnApplicationQuit()
    {
        ES3.Save(DropdownValue,_currentLocal);
    }
}
