using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    private string _currentLocal;
    private int _dropdownValue;
    
    private void Start()
    {
        _dropdown.value = _dropdownValue;
    }

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
        
        _dropdownValue = _dropdown.value;
        ES3AutoSaveMgr.Current.Save();
    }

    public void ChangeLanguage(string localeCode)
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
