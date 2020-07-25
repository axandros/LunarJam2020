using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static string DebugName = "[CdM]";
    [SerializeField]
    Draggable _dragComponent = null;
    public Draggable DragComponent { get { return _dragComponent; } }

    CardDisplay _display = null;

    [SerializeField]
    Card_SO _card = null;

    public Card_SO Card { get { return _card; }
        set { _card = value;
            if (_display) { _display.UpdateCard(_card); }
        } }

    float _cardTimer;

    public PurchaseStack _purchase = null;
    public PurchaseStack Purchase {
        get {
            //Debug.Log(DebugName+" Purchase Requested.  Drag: " + _dragComponent + " | Purchase: " + _dragComponent.Purchase);
            return _purchase;
        }
        set {
                if (_dragComponent) { _dragComponent.Purchase = value; }
                _purchase = value;
            }
        }
    

    // Start is called before the first frame update
    void Start()
    {
        _dragComponent = GetComponent<Draggable>();
        //Debug.Log("[CdM] Drag Component: " + dragComponent);
        _display = GetComponent<CardDisplay>();
        if (_display) { _display.UpdateCard(Card); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Execute(PlayerManager pm, PlayZone pz)
    {
        _cardTimer += Time.deltaTime;
        Debug.Log(DebugName + " Executing Card.");
        if(Card.Execute(_cardTimer, pm))
        {
            Debug.Log(DebugName + " Discarding Executed card.");
            pz.Discard(this);
        }
    }

    public bool PlayCard(PlayerManager pm, PlayZone pz)
    {
        Debug.Log(DebugName + " Card is being played.");
        _cardTimer = -Time.deltaTime;
        Execute(pm, pz);
        return true;
    }

    public void Redisplay()
    {
        if (_display) { _display.UpdateCard(_card); }
    }
}
    
