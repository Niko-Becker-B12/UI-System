using Cards;
using UnityEngine;
using UnityEngine.Events;

public class CardVisual : UiWindowModal
{

    public CardDataObject cardData;

    public UnityEvent<CardDataObject> OnCardPickedEvent;
    

    public void Initialize(CardDataObject newData, bool isServer)
    {

        cardData = newData;
        
        UiButton cardButton = windowBody.GetComponentInChildren<UiButton>();
            
        Function onClick = new Function
        {
            functionDelay = 0,
            functionName = new UnityEvent { }
        };
        onClick.functionName.AddListener(() =>
        {
            
            OnCardPickedEvent.Invoke(cardData);
                
        });

        cardButton.onClickFunctions.Add(onClick);
            
        if(!isServer)
            cardButton.gameObject.SetActive(false);

    }
    
}