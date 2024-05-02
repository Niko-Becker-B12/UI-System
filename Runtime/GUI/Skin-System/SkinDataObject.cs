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

    public string cmsUrl;
    public string cmsStartUrl;

    public bool listedAsCustomID = false;

    public Texture2D baseStreamThumbnail;

    public CustomPlayerIdBaseHandler idHandler;

    public TMP_StyleSheet textStyleSheet;
    public TMP_FontAsset fontAsset;

    public List<ComponentSkinDataObject> componentSkinDataObjects = new List<ComponentSkinDataObject>();

    public List<LevelDataObject> levels = new List<LevelDataObject>();

}