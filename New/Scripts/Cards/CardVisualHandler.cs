using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Cards
{
    public class CardVisualHandler : MonoBehaviour
    {
        
        public CardDataHandler cardDataHandler;

        public GameObject cardPrefab;
        public RectTransform cardParent;
        
        public List<UiElement> cards = new List<UiElement>();

        private int currentIndex = -1;
        

        private void Awake()
        {
            
            cardDataHandler.OnCardDataGenerated.AddListener(GenerateAllCards);
            
            cardDataHandler.OnCardReceivedEvent.AddListener(GenerateNewCardVisual);
            
        }

        public void GenerateAllCards(List<CardDataObject> newCards)
        {
            
            Debug.Log("Generating all cards");

            for (int i = 0; i < newCards.Count; i++)
            {

                GenerateNewCardVisual(newCards[i]);

            }
            
        }

        [Button]
        void GenerateNewCardVisual(CardDataObject cardData)
        {
            
            GameObject newCard = GameObject.Instantiate(cardPrefab, cardParent);
            CardVisual cardVisual = newCard.GetComponent<CardVisual>();
            
            cards.Add(cardVisual);
            
            cardVisual.Initialize(cardData, cardDataHandler.isServer);

            cardVisual.OnCardPickedEvent.AddListener(CardPicked);
            
            

        }

        void CardPicked(CardDataObject cardData)
        {
            
            cardDataHandler.OnCardPickedEvent.Invoke(cardData);
            
        }
        
    }
}