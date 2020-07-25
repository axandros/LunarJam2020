using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEvent", menuName = "Cards/CardEvent_PlayCard")]
public class PlayCard : CardEvent
{
    public override void Event(PlayerManager pm, GameManager gm)
    {
        pm.PlayCard();
    }
}
