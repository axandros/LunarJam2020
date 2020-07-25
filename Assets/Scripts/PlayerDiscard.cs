using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiscard : MonoBehaviour
{
    [SerializeField]
    short MaxSize = 99;

    [SerializeField]
    PlayerDeck MatchingDeck = null;

    [SerializeField]
    GameObject TopCard = null;

    [SerializeField]
    Vector3 HiddenLocation = new Vector3(0.0f, -10.0f, 0.0f);

    Vector3 _topCardLocation = Vector3.zero;

    List<CardManager> _discardContents = new List<CardManager>();
    
    

    // Start is called before the first frame update
    void Start()
    {
        if (TopCard) {
            _topCardLocation = TopCard.transform.position;
                }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCard(CardManager card)
    {
        if (!_discardContents.Contains(card)) 
        {
            _discardContents.Add(card);
        }
        Redisplay();
    }

    public void AddCards(List<CardManager> cardstoAdd)
    {
        if (cardstoAdd.Count > 0)
        {
            foreach (CardManager card in cardstoAdd)
            {
                _discardContents.Add(card);
            }
        }
        Redisplay();
    }

    public void Redisplay()
    {
        if (_discardContents.Count > 0) {
            foreach (CardManager card in _discardContents)
            {
                card.transform.position = HiddenLocation;
            }
            _discardContents[_discardContents.Count - 1].transform.position = _topCardLocation;
        }
    }

    public void ShuffleIntoDeck(PlayerDeck pd)
    {
        if (pd)
        {
            Debug.Log("[Dis] Shuffling Discard into Deck");
            pd.AddCardList(_discardContents);
            _discardContents.Clear();
        }
    }
}
