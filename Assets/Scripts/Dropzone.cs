using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropzone : MonoBehaviour
{
    /// <summary>
    /// The maximum number of cards that can be held.
    /// If the value is below 0, This will hold an infinite number of cards.
    /// </summary>
    [SerializeField]
    short Capacity = 0;

    /// <summary>
    /// Prevent the Player Draggable component from moving cards form here.
    /// </summary>
    [SerializeField]
    public bool Locked = true;


    //[SerializeField]
    static float cardWidth = 2.5f;
    //[SerializeField]
    static float cardPadding = 0.25f;



    protected List<Draggable> CardsInZone = new List<Draggable>();

    private void Start()
    {
        Draggable[] drags = GetComponentsInChildren<Draggable>();
        foreach (Draggable drag in drags){
            AddDraggable(drag);
        }
    }

    public virtual void AddDraggable(Draggable card)
    {
        if (Capacity < 0 || CardsInZone.Count < Capacity)
        {
            card.Parent = this;
            Debug.Log("Adding " + card.gameObject.name + " to " + this.gameObject.name);
            CardsInZone.Add(card);
            Reorganize();
        }
    }
    public virtual void RemoveDraggable(Draggable card)
    {
        Debug.Log("Removing " + card.gameObject.name + " from " + this.gameObject.name);
        CardsInZone.Remove(card);
        Reorganize();
    }

    public void Reorganize()
    {
        float TotalWidth = (cardWidth + 2 * cardPadding) * CardsInZone.Count;
        for(int i = 0; i < CardsInZone.Count; i++)
        {
            CardsInZone[i].transform.position = transform.position + new Vector3(1, 0, 0) * ((TotalWidth/2)-(TotalWidth / CardsInZone.Count)*i);
        }
    }

    
}
