using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public enum InsertPosition { Top, Bottom, Shuffle}

    [SerializeField]
    public GameObject CardPrefab = null;

    [System.Serializable]
    public class CardAmountEntry
    {
        public Card_SO Card;
        public short Amount;
    }
    [SerializeField]
    CardAmountEntry[] DeckList = new CardAmountEntry[0];

    [SerializeField]
    Vector3 HiddenLocation = new Vector3(0.0f, -10.0f, 0.0f);

    // Index into Decklist, instead of storing the card info twice.  
    List<CardManager> _currentDeck = new List<CardManager>();

    [SerializeField]
    PlayerDiscard AssociatedDiscard = null;

    // Start is called before the first frame update
    void Start()
    {
        for (short i = 0; i < DeckList.Length; i++) {
            CardAmountEntry cae = DeckList[i];
            for (short j = 0; j < cae.Amount; j++) {
                CardManager newCard = CreateCard(cae.Card);
                _currentDeck.Add(newCard);
                //Log("Adding " + cae.Card.Name + " to the deck");
                }
            }
        Shuffle();
    }

    public void DrawCard(PlayerHand drawZone)
    {
        if (_currentDeck.Count > 0)
        {
            CardManager drawnCard = _currentDeck[0];
            _currentDeck.RemoveAt(0);
            drawZone.AddCard(drawnCard);
            Debug.Log("[Dck] Drawing card: " + drawnCard.Card.name + ". Deck now has " + _currentDeck.Count +" cards.");
        }
        else if (AssociatedDiscard)
        {
            AssociatedDiscard.ShuffleIntoDeck(this);
            if (_currentDeck.Count > 0)
            {
                CardManager drawnCard = _currentDeck[0];
                _currentDeck.RemoveAt(0);
                drawZone.AddCard(drawnCard);
                Debug.Log("[Dck] Drawing card: " + drawnCard.Card.name + ". Deck now has " + _currentDeck.Count + " cards.");
            }
        }
    }

    public void Shuffle()
    {
        List<CardManager> shuffledDeck = new List<CardManager>();
        while(_currentDeck.Count > 0)
        {
            int rand  = (int)(Random.Range(0, _currentDeck.Count - 1));
            shuffledDeck.Add(_currentDeck[rand]);
            _currentDeck.RemoveAt(rand);
        }
        _currentDeck = shuffledDeck;
    }

    /// <summary>
    /// Shuffle a list of cards into the deck
    /// </summary>
    /// <param name="CardList"></param>
    /// <returns></returns>
    public bool AddCardList(List<CardManager> cardList, bool shuffle = true)
    {
        for(int i = 0; i < cardList.Count; i++)
        {
            _currentDeck.Add(cardList[i]);
            SetCardPosition(cardList[i]);
        }
        if (shuffle) { Shuffle(); }
        return true;
    }

    public void AddCard(CardManager card, bool shuffle = true)
    {
        if (card)
        {
            _currentDeck.Add(card);
            SetCardPosition(card);
        }
        if (shuffle) { Shuffle(); }
    }

    public CardManager CreateCard(Card_SO cardSO)
    {
        GameObject drawnCard = GameObject.Instantiate(CardPrefab);
        drawnCard.transform.localScale = new Vector3(1, 1, 1);
        CardManager card = drawnCard.GetComponent<CardManager>(); 
        card.Card = cardSO;
        SetCardPosition(card);
        return card;
    }

    void SetCardPosition(CardManager card)
    {
        card.transform.position = this.transform.position + HiddenLocation;
    }
}
