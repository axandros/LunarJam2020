using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField]
    Text Name = null;

    [SerializeField]
    Text Cost = null;

    [SerializeField]
    Text Type = null;

    [SerializeField]
    Text Description = null;

    [SerializeField]
    SpriteRenderer CardArt = null;

    public void UpdateCard(Card_SO card)
    {
        if (card)
        {
            if (Name) { Name.text = card.Name; }
            if (Cost) { Cost.text = card.PuchasePrice.ToString(); }
            if (Type)
            {
                switch(card.Type){
                    case (CardType.Event):
                        Type.text = "Wolf";
                        break;
                    case (CardType.Location):
                        Type.text = "Location";
                        break;
                } 
            }
            if (Description) { Description.text = card.Description; }
            if (CardArt) { CardArt.sprite = card.CardArt; }
        }
        
    }
}
