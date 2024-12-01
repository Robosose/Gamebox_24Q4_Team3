using Patterns.Singleton;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSwitcher : Singleton<LanguageSwitcher>
{
    private string _enLocal = "en";
    private string _ruLocal = "ru";
    private string _currentLocal = "en";

    public string RuLocal => _ruLocal;
    public string EnLocal => _enLocal;
    public string CurrentLocal => _currentLocal;
    
    private const string DropdownValue = "DropdownValue";

    private void Start()
    {
        if(ES3.FileExists(Application.persistentDataPath))
            if (ES3.KeyExists(DropdownValue))
                _currentLocal = ES3.Load<string>(DropdownValue);
        
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
    
    public void ChangeLanguage()
    {
        _currentLocal = _currentLocal == _enLocal?_ruLocal:_enLocal;
        var selectedLocale = LocalizationSettings.AvailableLocales.GetLocale(_currentLocal);
        if (selectedLocale != null)
        {
            LocalizationSettings.SelectedLocale = selectedLocale;
            ES3.Save(DropdownValue,_currentLocal);
        }
        else
        {
            Debug.LogWarning($"Locale '{_currentLocal}' not found.");
        }
    }

    private void OnApplicationQuit()
    {
        ES3.Save(DropdownValue,_currentLocal);
    }
}
