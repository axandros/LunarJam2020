using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDragger : MonoBehaviour
{
    Draggable _currentlyDragging = null;

    [SerializeField]
    LayerMask CardMask = new LayerMask();
    [SerializeField]
    LayerMask CardSlotMask = new LayerMask();
    [SerializeField]
    LayerMask BoardMask = new LayerMask();
    [SerializeField]
    LayerMask DeckMask = new LayerMask();

    [SerializeField]
    PlayerDeck PlayersDeck = null;

    [SerializeField]
    PlayerHand PlayersHand = null;

    [SerializeField]
    PlayZone Board = null;

    [SerializeField]
    PlayerManager ThePlayer = null;


    private void Update()
    {
        CardDrag();
        bool PlayerClick = Input.GetMouseButtonDown(0);
        if (PlayerClick)
        {
            EndTurn();
            //DeckDraw();
        }
        else{
            CardPreview();
        }
    }

    /// <summary>
    /// The primary loop for dealing with the player clicking on a card and dragging it.
    /// This occurs when Teh player is buying a card or playing a card.
    /// </summary>
    void CardDrag()
    {

        bool click = Input.GetMouseButton(0);
        bool mouseRelease = Input.GetMouseButtonUp(0);
        if (_currentlyDragging == null && click)
        {
            Draggable drag = FindDragableFromScreenSpace(Input.mousePosition);
            if (drag && !drag.Locked) {
                _currentlyDragging = drag;
                _currentlyDragging.Selected = true;
            }
        }
        DragCard();
        if (mouseRelease)
        {
            DropIntoZone();
        }
        if (!click && _currentlyDragging)
        {
            _currentlyDragging.Selected = false;
            _currentlyDragging = null;
        }
    }

    void DropIntoZone()
    {
        Ray ScreenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastOut;
        if (_currentlyDragging && Physics.Raycast(ScreenRay, out raycastOut, Mathf.Infinity, CardSlotMask))
        {
            PlayZone dz = raycastOut.collider.gameObject.GetComponent<PlayZone>();
            if (dz && !dz.Locked)
            {
                dz.AddDraggable(_currentlyDragging);
            }
        }
    }

    /// <summary>
    /// Intended to find a Draggable Item beneath the mouse pointer, passed tinto the function as a Vector3
    /// </summary>
    /// <param name="MousePosition"></param>
    /// <returns></returns>
    private Draggable FindDragableFromScreenSpace(Vector3 MousePosition)
    {
        Draggable ret = null;

        Ray ScreenRay = Camera.main.ScreenPointToRay(MousePosition);
        RaycastHit raycastOut;
        if (Physics.Raycast(ScreenRay, out raycastOut, Mathf.Infinity, CardMask)) {
            ret = raycastOut.collider.gameObject.GetComponent<Draggable>();
            
            Debug.Log("Found " + ret);
        }
        return ret;
    }

    private void DragCard()
    {
        Ray ScreenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastOut;
        if (_currentlyDragging && Physics.Raycast(ScreenRay, out raycastOut, Mathf.Infinity, BoardMask | CardSlotMask))
        {
            _currentlyDragging.MoveTarget = raycastOut.point;
            _currentlyDragging.MoveToTarget();
        }
    }

    void DeckDraw()
    {
        RaycastHit hitOut;
        if (Utility.ScreenRaycast(out hitOut, DeckMask) && PlayersHand)
        {
            PlayerDeck dc = hitOut.collider.gameObject.GetComponent<PlayerDeck>();
            if (dc)
            {
                dc.DrawCard(PlayersHand);
            }
        }
    }

    void EndTurn()
    {
        RaycastHit hitOut;
        if (Utility.ScreenRaycast(out hitOut, DeckMask) && PlayersHand)
        {
            EndTurnButton dc = hitOut.collider.gameObject.GetComponent<EndTurnButton>();
            if (dc)
            {
                ThePlayer.EndTurn();
            }
        }
    }

    public void CardPreview()
    {
        RaycastHit hitOut;
        Card_SO preview = null;
        if (Utility.ScreenRaycast(out hitOut, CardMask))
        {
            CardManager dc = hitOut.collider.gameObject.GetComponent<CardManager>();
            if (dc)
            {
                preview = dc.Card;
            }
            
        }
        ThePlayer.PreviewCard = preview;
    }
}
