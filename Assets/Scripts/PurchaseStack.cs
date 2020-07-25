using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseStack : MonoBehaviour
{
    [SerializeField]
    GameObject CardPrefab = null;

    [SerializeField]
    Card_SO CardForPurchase;

    [SerializeField]
    short AvailableForPurchase = 10;

    [SerializeField]
    Vector3 CardDisplayLocation = new Vector3(0, 2, 0);

    List<CardManager> CardInStack = null;

    private void Start()
    {
        CardInStack = new List<CardManager>();
        for (short i = 0; i < AvailableForPurchase; i++)
        {
            Debug.Log("[PSt] Purchase Stack Creating Card");
            GameObject cardObject = Instantiate(CardPrefab);
            CardManager card = cardObject.GetComponent<CardManager>();
            if (!card) {
                Debug.Log("[PSt] Card Manager not Found");
                break;
                    }
            card.Card = CardForPurchase;
            card.transform.localScale = new Vector3(1, 1, 1);
            card.Purchase = this;
            CardInStack.Add(card);
        }
        Redisplay();
    }

    public bool Purchase(CardManager card)
    {
        bool ret = false;
        if (CardInStack.Contains(card))
        {
            ret = true;
            CardInStack.Remove(card);
        }
        Redisplay();
        return ret;
    }

    public void Redisplay()
    {
        if (CardInStack.Count > 0) {
            foreach (CardManager card in CardInStack)
            {
                card.transform.position = this.transform.position + new Vector3(0.0f, -10, 0);
            }
            CardInStack[0].transform.position = this.transform.position + CardDisplayLocation;
         }
        else { this.gameObject.SetActive(false); }
    }
}

