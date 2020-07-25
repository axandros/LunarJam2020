using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Location, Event}

[CreateAssetMenu(fileName ="newCard",menuName ="Cards/newCard")]
public class Card_SO : ScriptableObject
{
    [Tooltip("The Display name of the Card")]
    [SerializeField]
    string _name = "CardName";
    public string Name { get { return _name; } }

    [Tooltip("The card type")]
    [SerializeField]
    CardType _cardType = CardType.Location;
    public CardType Type { get { return _cardType; } }

    [Tooltip("The price to purchase the card")]
    [SerializeField]
    short _purchasePrice = 0;
    public short PuchasePrice { get { return _purchasePrice; } }

    [Tooltip("How many cards should be drawn each trigger")]
    [SerializeField]
    short _extraDraw = 0;
    public short ExtraDraw { get { return _extraDraw; } }

    [Tooltip("The amount of Gold provided each trigger")]
    [SerializeField]
    short _income=0;
    public short Income { get { return _income; } }

    [Tooltip("How long a location should remain on the board.\nIgnored for Events")]
    [SerializeField]
    float _lifespan=0;
    public float Lifespan { get { return _lifespan; } }

    [Tooltip("How many time a location should trigger while on the board.\nIgnored for Events")]
    [SerializeField]
    short _triggersPerLife = 1;
    public short Triggers { get { return _triggersPerLife; } }

    public float TimeBetweenTriggers { get { return Lifespan/ Triggers; } }


    [Tooltip("A description of the effects of the card.")]
    [Multiline]
    [SerializeField]
    string _description = "";
    public string Description { get { return _description;  } }

    [Tooltip("Lore, jokes, and witty camp.")]
    [SerializeField]
    string _flavor = "";
    public string FlavorText { get { return _flavor; } }

    [Tooltip("The art for the card.\nThis is what will be read every time a player looks at the card.")]
    [SerializeField]
    Sprite _cardArt = null;
    public Sprite CardArt { get { return _cardArt; } }

    [SerializeField]
    CardEvent[] Events;

    [SerializeField]
    CardEvent[] FullMoonEvents;



    public virtual bool Execute(float lifetime, PlayerManager pm)
    {
        bool ret = false;
        if (_cardType == CardType.Event)
        {
            normalExecutions(pm);
            ret = true;
        }
        else {
            float v1 = lifetime % TimeBetweenTriggers;
            float v2 = (lifetime - Time.deltaTime) % TimeBetweenTriggers;
            if (v1 < v2)
            {
                normalExecutions(pm);
            }

            if (lifetime > Lifespan )
            {
                ret = true;
            }
        }
        return ret;
    }

    private void normalExecutions(PlayerManager pm)
    {
        GameManager gm = GameManager.Instance();
        if (gm.CurrentPhase == LunarPhase.Full)
        {
            foreach (CardEvent evnt in FullMoonEvents)
            {
                evnt.Event(pm, gm);
            }
        }
        else
        {
            foreach (CardEvent evnt in Events)
            {
                evnt.Event(pm, gm);
            }
        }
        /*
        pm.Gold += Income;
        for (short i = 0; i < ExtraDraw; i++) {
            pm.DrawFromDeck();
                }
                */
    }
}
