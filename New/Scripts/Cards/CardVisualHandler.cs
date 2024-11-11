using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Cards
{
    public class CardVisualHandler : MonoBehaviour
    {
        
        CardDataHandler cardDataHandler => FindAnyObjectByType<CardDataHandler>();

        public GameObject cardPrefab;
        public RectTransform cardParent;
        
        public List<UiElement> cards = new List<UiElement>();


        private void Start()
        {
            
            cardDataHandler.OnCardReceivedEvent.AddListener(GenerateNewCardVisual);
            
        }

        [Button]
        void GenerateNewCardVisual(CardDataObject cardData)
        {
            
            GameObject newCard = GameObject.Instantiate(cardPrefab, cardParent);
            UiWindowModal cardVisual = newCard.GetComponent<UiWindowModal>();
            
            cards.Add(cardVisual);
            
            UiButton cardButton = cardVisual.windowBody.GetComponentInChildren<UiButton>();
            
            Function onClick = new Function
            {
                functionDelay = 0,
                functionName = new UnityEvent { }
            };
            onClick.functionName.AddListener(() =>
            {
                
                if(cardDataHandler.isServer)
                    cardDataHandler.OnCardPickedEvent.Invoke(cardData);
                
            });

            cardButton.onClickFunctions.Add(onClick);
            
            if(cardDataHandler.isServer)
                cardButton.gameObject.SetActive(false);
            
        }
        
    }
}