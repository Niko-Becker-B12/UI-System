using System;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;
using UnityEngine.Events;

namespace Cards
{
    public class CardDataHandler : MonoBehaviour
    {
        private DatabaseHandler databaseHandler;
        
        public List<CardDataObject> cardDataObjects = new List<CardDataObject>();
        public List<CardTag> tags = new List<CardTag>();

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
            tags = loadedTags;
            
            LoadData();

        }

        void LoadData()
        {
            
            Debug.Log("Loading card data");
            
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