using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEvent", menuName = "Cards/CardEvent_WinGame")]
public class WinGame : CardEvent
{
    public override void Event(PlayerManager pm, GameManager gm)
    {
        gm.WinTheGame();
    }
}
