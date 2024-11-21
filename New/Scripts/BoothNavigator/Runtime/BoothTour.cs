using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace BoothNavigator
{

    [System.Serializable]
    public class BoothTour
    {

        public List<BoothTopic> topics = new List<BoothTopic>();

        [ReadOnly]
        public bool isLocked = true;

        public bool AddTopicToTour(BoothTopic topic)
        {

            if(topics.Contains(topic))
                return false;

            topics.Add(topic);

            return true;

        }

        public bool RemoveTopicFromTour(BoothTopic topic)
        {

            if(!topics.Contains(topic)) 
                return false;

            topics.Remove(topic);

            return true;

        }

    }


}