using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Settings
{

    private string name;

    public string Name
    {

        get { return name; }
        private set { name = value; }

    }

    public string value;

}