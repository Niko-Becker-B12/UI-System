using Hextant;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
using NUnit.Framework.Internal;
using System;

#if UNITY_EDITOR
using Hextant.Editor;
using UnityEditor;
#endif

[Settings(SettingsUsage.RuntimeProject, "B12/Game Settings")]
public sealed class RuntimeSettings : Settings<RuntimeSettings>
{

#if UNITY_EDITOR
        [SettingsProvider]
        public static SettingsProvider GetSettingsProvider() => instance.GetSettingsProvider();
#endif


    public List<RuntimeSettingsPropertyBase> properties = new List<RuntimeSettingsPropertyBase>();

    /*
    [OdinSerialize, PolymorphicDrawerSettings(ShowBaseType = false)]
    [ShowInInspector]
    [SerializeField]
    public Dictionary<string, RuntimeSettingsPropertyBase> properties = new Dictionary<string, RuntimeSettingsPropertyBase>();


    public string GetPropertyValue(string propertyName)
    {

        if (propertyName == null || string.IsNullOrWhiteSpace(propertyName))
            return string.Empty;

        RuntimeSettingsPropertyBase foundProperty = GetProperty(propertyName);

        if (foundProperty == null)
            return string.Empty;

        return foundProperty.value.ToString();

    }

    public RuntimeSettingsPropertyBase GetProperty(string propertyName)
    {

        if (propertyName == null || string.IsNullOrWhiteSpace(propertyName))
            return null;

        RuntimeSettingsPropertyBase foundProperty;
        
        if(properties.TryGetValue(propertyName, out foundProperty))
            return foundProperty;
        else
            return null;

    }

    public void SetPropertyValue(string propertyName, string propertyValue)
    {

        if (propertyName == null || string.IsNullOrWhiteSpace(propertyName))
            return;

        RuntimeSettingsPropertyBase foundProperty = GetProperty(propertyName);

        if (foundProperty == null)
            return;

        foundProperty.value = propertyValue;

        SaveSettingsToJson();

    }

    public void SetPropertyValue(string propertyName, RuntimeSettingsPropertyBase newProperty)
    {

        if (propertyName == null || string.IsNullOrWhiteSpace(propertyName))
            return;

        RuntimeSettingsPropertyBase foundProperty = GetProperty(propertyName);

        if (foundProperty == null)
            return;

        foundProperty.value = newProperty.value;

        SaveSettingsToJson();

    }

    private void OnEnable()
    {

#if UNITY_EDITOR
        SaveSettingsToJson();
#else
        if (File.Exists($"{Application.persistentDataPath}/B12-Settings.json"))
        {

            string jsonString = File.ReadAllText($"{Application.persistentDataPath}/B12-Settings.json");

            JsonUtility.FromJsonOverwrite(jsonString, RuntimeSettings.instance);

        }
        else
        {

            SaveSettingsToJson();

        }
#endif

    }

    */


    public string GetPropertyValue(string propertyName)
    {

        if (propertyName == null || string.IsNullOrWhiteSpace(propertyName))
            return string.Empty;

        RuntimeSettingsPropertyBase foundProperty = GetProperty(propertyName);

        if (foundProperty == null)
            return string.Empty;

        return foundProperty.Value.ToString();

    }

    public RuntimeSettingsPropertyBase GetProperty(string propertyName)
    {

        if (propertyName == null || string.IsNullOrWhiteSpace(propertyName))
            return null;

        RuntimeSettingsPropertyBase foundProperty = properties.Find((x) => x.name == propertyName);

        return foundProperty;

    }

    public void SetPropertyValue(string propertyName, string propertyValue)
    {

        if (propertyName == null || string.IsNullOrWhiteSpace(propertyName))
            return;

        RuntimeSettingsPropertyBase foundProperty = GetProperty(propertyName);

        if (foundProperty == null)
            return;

        foundProperty.Value = propertyValue;

        SaveSettingsToJson();

    }

    public void SetPropertyValue(string propertyName, RuntimeSettingsPropertyBase newProperty)
    {

        if (propertyName == null || string.IsNullOrWhiteSpace(propertyName))
            return;

        RuntimeSettingsPropertyBase foundProperty = GetProperty(propertyName);

        if (foundProperty == null)
            return;

        foundProperty.Value = newProperty.Value;

        SaveSettingsToJson();

    }

    private void OnEnable()
    {

#if UNITY_EDITOR
        SaveSettingsToJson();
#else
        if (File.Exists($"{Application.persistentDataPath}/B12-Settings.json"))
        {

            string jsonString = File.ReadAllText($"{Application.persistentDataPath}/B12-Settings.json");

            JsonUtility.FromJsonOverwrite(jsonString, RuntimeSettings.instance);

        }
        else
        {

            SaveSettingsToJson();

        }
#endif

    }

    [Button("Save Settings")]
    public static void SaveSettingsToJson()
    {

        Debug.Log("Saving Settings");

        string jsonString = JsonUtility.ToJson(RuntimeSettings.instance, true);

        File.WriteAllText($"{Application.persistentDataPath}/B12-Settings.json", jsonString);

    }

    [Button("Update Settings")]
    public void UpdateSettings()
    {

        for(int i = 0; i < properties.Count; i++)
        {

            properties[i].Value = properties[i].Value;

        }

    }

}

[System.Serializable]
public class RuntimeSettingsPropertyBase
{

    public string name;

    [SerializeField]
    private string value;

    public string Value
    {

        get => value; 
        set 
        { 
            this.value = value; 
            OnPropertyChanged?.Invoke(value); 
        }
    

    }

    public event Action<string> OnPropertyChanged;

}

public interface ISettingsProperty<T>
{

    public T Value { get; set; }

}