using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Cards
{
    public class CardDataHandler : MonoBehaviour
    {
        private DatabaseHandler databaseHandler;
        
        public List<CardDataObject> cardDataObjects = new List<CardDataObject>();
        public List<CardTag> loadedTagsDatabase = new List<CardTag>();
        
        public List<CardDataObject> usableCardDataObjects = new List<CardDataObject>();
        public List<CardTag> usableCardTags = new List<CardTag>();

        public UnityEvent<List<CardDataObject>> OnCardDataGenerated;
        public UnityEvent<CardDataObject> OnCardPickedEvent;
        public UnityEvent<CardDataObject> OnCardReceivedEvent;

        public bool isServer = true;

        private void Start()
        {
            
            databaseHandler = new DatabaseHandler();

            databaseHandler.OnLoadingStarted += GetData;
            databaseHandler.OnLoadingFinished += InitializeData;

            OnCardReceivedEvent.AddListener(CardReceivedEvent);
            
            databaseHandler.Initialize();

        }

        void GetData()
        {
            
            Debug.Log("CardDataHandler.GetData()");
            
        }

        void InitializeData(List<CardDataObject> loadedCards, List<CardTag> loadedTags)
        {

            cardDataObjects = loadedCards;
            loadedTagsDatabase = loadedTags;
            
            for (int i = 0; i < loadedCards.Count; i++)
            {

                for (int j = 0; j < loadedCards[i].tags.Count; j++)
                {
                    
                    if(usableCardTags.Find((x)=> x.Name == loadedCards[i].tags[j].Name) != null)
                        usableCardDataObjects.Add(loadedCards[i]);
                    
                }
                
            }
            
            LoadData();

        }

        void LoadData()
        {
            
            OnCardDataGenerated?.Invoke(usableCardDataObjects);
            
        }

        public void SendCardPickedEvent(CardDataObject cardDataObject)
        {
            
            //Server sendMessage
            
        }

        public void CardReceivedEvent(CardDataObject cardDataObject)
        {
            
            //Only on Client
            
            
            
        }
        
    }
}