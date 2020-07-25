using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    PlayerDeck TheDeck = null;

    [SerializeField]
    PlayerDiscard TheDiscard = null;

    [SerializeField]
    PlayZone TheBoard = null;

    [SerializeField]
    PlayerHand TheHand = null;

    [SerializeField]
    Text DebugText = null;

    [SerializeField]
    GameManager TheGameManager = null;

    [SerializeField]
    short StartingCards = 3;

    [SerializeField]
    FoodDisplay _foodReadout = null;

    [SerializeField]
    CardManager _previewCard = null;
    public Card_SO PreviewCard
    {
        get { return _previewCard.Card; }
        set { 
            if (_previewCard)
                if(value)
                {
                    _previewCard.Card = value;
                    _previewCard.gameObject.SetActive(true);
                }
                else
                {
                    _previewCard.gameObject.SetActive(false);
                }
        }
    }

    short _currentGold = 0;
    public short Gold { get { return _currentGold; } set
        {
            Debug.Log("Setting Gold: " + value);
            if (value < 0) { _currentGold = 0; }
            else { _currentGold = value; }
            Debug.Log("Gold set to :" + Gold);
            UpdateDebugText();
        }   }

    short _currentMilitia = 0;
    public short Militia
    { get { return _currentMilitia; }set
        { if (value < 0) { _currentMilitia = 0; }
            else { _currentMilitia = value; }
            UpdateDebugText();
        } }

    // Start is called before the first frame update
    void Start()
    {
        if (!TheDeck) { TheDeck = GetComponentInChildren<PlayerDeck>(); }
        if (!TheDiscard) { TheDiscard = GetComponentInChildren<PlayerDiscard>(); }
        if(!TheDeck || !TheDiscard || !TheBoard)
        {
            Debug.LogError(this.gameObject.name + " does not have all the required components!");
        }
    }
    private void Update()
    {
        UpdateDebugText();
    }

    void UpdateDebugText()
    {
        if (DebugText && false)
        {
            DebugText.text = string.Format("Gold: "+Gold+"\nMilitia: "+ Militia + "\n Lunar Phase: " + TheGameManager.CurrentPhase.ToString());
        }
        if (_foodReadout)
        {
            _foodReadout.Redisplay(Gold);
        }
    }

    public void PurchaseCard(PurchaseStack ps)
    {

    }

    public void PregameSetup()
    {
        Debug.Log("Pregame Setup for " + this.name);
        for (int i = 0; i < 5; i++)
        {
            TheDeck.DrawCard(TheHand);
        }
    }

    public void StartTurn()
    {
        
    }

    public void EndTurn()
    {
        // Discard All Cards
        if (TheHand)
        {
            TheHand.DiscardAll();
            Debug.Log("[PlM] Discarding the hand.");
        }
        TheBoard.DiscardAll();
        // Draw 5 Cards
        for(short i = 0; i < StartingCards; i++)
        {
            //Debug.Log("Draw");
            TheDeck.DrawCard(TheHand);
        }
        Gold = 0;
        Debug.Log("[PlM] Player ending turn, messaging Game Master.");
        TheGameManager.EndTurn(this);
    }

    public void DrawFromDeck()
    {
        TheDeck.DrawCard(TheHand);
    }

    public void PlayCard()
    {
        TheHand.PlayCard();
    }
}
