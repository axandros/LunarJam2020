using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "newEvent", menuName ="Cards/newEvent")]
public abstract class CardEvent : ScriptableObject
{
    public abstract void Event(PlayerManager pm, GameManager gm);
}
