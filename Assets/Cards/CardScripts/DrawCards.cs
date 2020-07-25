using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEvent", menuName = "Cards/CardEvent_Draw")]
public class DrawCards : CardEvent
{
    [SerializeField]
    short Number = 1;

    public override void Event(PlayerManager pm, GameManager gm)
    {
        for (short i = 0; i < Number; i++)
        {
            pm.DrawFromDeck();
        }
    }
}
