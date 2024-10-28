using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[System.Serializable]
[Sirenix.OdinInspector.InlineEditor]
[CreateAssetMenu(fileName = "New Skin DataObject", menuName = "Skin/Skin DataObject")]
public class SkinDataObject : ScriptableObject
{

    [SerializeField]
    private string clientName;

    public string ClientName
    {

        get
        {
            return clientName;
        }

        private set
        {
            clientName = value;
        }

    }

    [SerializeField]
    private string clientPassword;

    public string ClientPassword
    {

        get
        {
            return clientPassword;
        }

        private set
        {
            clientPassword = value;
        }

    }

    public bool listedAsCustomID = false;

    public TMP_StyleSheet textStyleSheet;
    public TMP_FontAsset fontAsset;

    public List<ComponentSkinDataObject> componentSkinDataObjects = new List<ComponentSkinDataObject>();

}