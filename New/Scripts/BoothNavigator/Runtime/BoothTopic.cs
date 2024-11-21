using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization;

namespace BoothNavigator
{

    [System.Serializable]
    public class BoothTopic
    {

        [TitleGroup("@title")]
        [HorizontalGroup("@title/1")]
        [VerticalGroup("@title/1/3")]
        public LocalizedString title;
        [VerticalGroup("@title/1/3"), TextArea()]
        public string description;
        [VerticalGroup("@title/1/3")]
        public Vector3 position;

        [VerticalGroup("@title/1/2"), PreviewField(Height = 200f), HideLabel]
        public Sprite icon;

    }


}