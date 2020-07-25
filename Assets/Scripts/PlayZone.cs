using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayZone : Dropzone
{
    [SerializeField]
    PlayerDeck AssociatedDeck = null;
    [SerializeField]
    PlayerDiscard AssociatedDiscard = null;
    [SerializeField]
    PlayerManager AssociatedManager = null;

    public override void AddDraggable(Draggable drag)
    {
        CardManager card = drag.GetComponent<CardManager>();
        if (card)
        {
            if (card.Purchase != null)
            { // Card is being purchased
                Debug.Log("[Plz] Trying to purchase card " + card.Card.name);
                if (AssociatedManager.Gold >= card.Card.PuchasePrice)
                {
                    if (card.Purchase.Purchase(card)){
                        AssociatedManager.Gold -= card.Card.PuchasePrice;
                        AssociatedDiscard.AddCard(card);
                        card.Purchase.Redisplay();
                        card.Purchase = null;
                        Debug.Log("[PlZ] Purchased: " + card.Card.name + ".");
                    }
                }
                else
                {
                    Debug.Log("[PlZ] Trying to buy " + card.Card.name + " but not enough Gold.");
                    card.Purchase.Redisplay();
                }
            }
            else
            {
                base.AddDraggable(drag);
                Debug.Log("[PlZ] PlayingCard: " + card.Card.name);
                card.PlayCard(AssociatedManager, this);
                drag.Locked = true;
            }
        }
    }

    public void DiscardAll()
    {
        List<CardManager> cardList = new List<CardManager>();
        foreach(Draggable drag in CardsInZone)
        {
            CardManager card = drag.GetComponent<CardManager>();
            if (card)
            {
                cardList.Add(card);
            }
        }
        CardsInZone.Clear();
        AssociatedDiscard.AddCards(cardList);
    }
    public void Discard(CardManager card)
    {
        AssociatedDiscard.AddCard(card);
        CardsInZone.Remove(card.DragComponent);
    }

    public void Update()
    {
        foreach(Draggable drag in CardsInZone)
        {
            CardManager card = drag.GetComponent<CardManager>();
            if (card)
            {
                card.Execute(AssociatedManager, this);
            }
        }
    }

    //public void ShuffleToDeck() { }

}
