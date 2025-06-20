using Sirenix.OdinInspector;
using UnityEngine;

public class RuntimeSettingsWrapper : SerializedMonoBehaviour
{

    public string propertyName;

    [HideInInspector]
    public RuntimeSettingsPropertyBase property;

    public virtual void Start()
    {

        property = RuntimeSettings.instance.GetProperty(propertyName);

        if (property != null)
        {

            //call to set the value of our example button/toggle
            OnValueChangedExternally(property.Value);

            OnEnable();

        }

    }

    public virtual void OnEnable()
    {

        RuntimeSettingsPropertyBase property = RuntimeSettings.instance.GetProperty(propertyName);

        if (property != null)
            property.OnPropertyChanged += OnValueChangedExternally;

    }

    public virtual void OnDisable()
    {

        RuntimeSettingsPropertyBase property = RuntimeSettings.instance.GetProperty(propertyName);

        if (property != null)
            property.OnPropertyChanged -= OnValueChangedExternally;

    }

    //Run this, when changing locally -> i.e a button changes a setting
    public virtual void OnValueChanged(RuntimeSettingsPropertyBase property)
    {

        Debug.Log($"Property was changed locally to {property.Value}");

        RuntimeSettings.instance.SetPropertyValue(propertyName, property.Value);

    }

    //this gets run, if the value was changed externally, i.e a differen button changed, so this button needs to reflect that change
    public virtual void OnValueChangedExternally(string value)
    {

        if (value == property.Value)
            return;

        Debug.Log($"Property was changed externally to {value}");

    }

    public virtual void UpdateValue(string value)
    {

        property.Value = value;

        OnValueChanged(property);

    }


}