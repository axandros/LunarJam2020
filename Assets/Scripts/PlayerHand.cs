using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : Dropzone
{
    [SerializeField]
    PlayZone board = null;
    [SerializeField]
    PlayerDiscard AssociatedDiscard = null;
    [SerializeField]
    PlayZone AssociatedZone = null;

    public void AddCard(CardManager card)
    {
        Draggable drag = card.GetComponent<Draggable>();
        if (drag) {
            //Debug.Log("adding draggable: " + drag.gameObject.name);
            AddDraggable(drag);
            Reorganize();
        }
    }
    public override void AddDraggable(Draggable card)
    {
        base.AddDraggable(card);
        card.Locked = false;
    }

    /*
    public override void RemoveDraggable(Draggable card)
    {
        base.RemoveDraggable(card);
    }

    public void Play(){

    }
    */

    public void Discard(){   }
    public void DiscardAll()
    {
        Debug.Log("[PlH] Discarding Hand");
        if (AssociatedDiscard)
        {
            while(CardsInZone.Count > 0)
            {
                Draggable drag = CardsInZone[0];
                CardsInZone.RemoveAt(0);
                drag.Locked = true;
                AssociatedDiscard.AddCard(drag.GetComponent<CardManager>());
            }
        }
    }

    public void PlayCard()
    {
        if (AssociatedZone)
        {
            Draggable card = CardsInZone[0];
            AssociatedZone.AddDraggable(card);
            CardsInZone.Remove(card);
        }
    }

    //public void SendtoDeck() { }
}
