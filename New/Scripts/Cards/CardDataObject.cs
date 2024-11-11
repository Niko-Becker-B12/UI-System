using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Cards/Card Data")]
    public class CardDataObject : ScriptableObject
    {

        public string name;
        public string description;
        public Sprite icon;
        public Guid guid;

        public List<CardTag> tags = new List<CardTag>();


        public bool CompareTags(List<CardTag> compareTags)
        {

            for (int i = 0; i < compareTags.Count; i++)
            {

                if (tags.Contains(compareTags[i]))
                {
                    return true;
                }
                
            }
            
            return false;

        }

    }

    [System.Serializable]
    public class CardTag
    {

        [SerializeField]
        private string name;

        public string Name
        {
            
            get { return name; }
            private set { name = value; }
            
        }
        
        [SerializeField]
        private Sprite icon;
        
        public Sprite Icon
        {
            
            get { return icon; }
            private set { icon = value; }
            
        }

        public CardTag(string name, Sprite icon)
        {
            
            Name = name;
            Icon = icon;
            
        }

    }
    
}