using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageLocaleSwitchHandler : MonoBehaviour
{

    public void ChangeUsedLocale(string locale)
    {

        int localeIndex = LocalizationSettings.AvailableLocales.Locales.FindIndex((x) => x.Identifier == locale);

        if(localeIndex != -1)
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeIndex];
        else
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];

    }

}