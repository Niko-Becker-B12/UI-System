using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace BoothNavigator
{

    [System.Serializable]
    public class BoothArea
    {

        [TitleGroup("@title")]
        [HorizontalGroup("@title/1")]
        [VerticalGroup("@title/1/2")]
        public string title;

        [VerticalGroup("@title/1/3")]
        public ComponentSkinDataObject iconSkinObject;


        public List<BoothTopic> topics = new List<BoothTopic> ();

    }


}