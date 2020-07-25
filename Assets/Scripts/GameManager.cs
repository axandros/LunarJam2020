using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LunarPhase { Waning, New, Waxing, Full }

public class GameManager : MonoBehaviour
{
    private static string DebugName = "[GmM]";
    private static GameManager _instance = null;
    public static GameManager Instance()
    {
        if (!_instance) {
            GameManager go = FindObjectOfType<GameManager>();
            if (go) { _instance = go; }
            else { //Make a new one?
                Debug.LogError(DebugName + "GameManager Unable to find or Instantiate a GameManager Instance.");
            }
        }
        return _instance;
    }

    List<PlayerManager> Player = new List<PlayerManager>();

    float _singlePlayertimer = 5.0f;
    //[SerializeField]
    //float TimeBetweenTurns = 5.0f;

    [SerializeField]
    Image VictoryPanel = null;
    bool _gameWon = false;

    [SerializeField]
    float DrawFrequency = 10.0f;
    float _drawTimer = 0;
    public float TimerDrawCard
    {
        get { return _drawTimer; }
        set {
            if (value < 0) { _drawTimer = 0; }
            else if(value > DrawFrequency)
            {
                Player[0].DrawFromDeck();
                TimerDrawCard = value - DrawFrequency;
            }
            else { _drawTimer = value; }
            }
    }

    /// <summary>
    /// The amount of time it takes to go from Full moon to new moon and back.
    /// </summary>
    [SerializeField]
    float LunarMonthTime = 120;
    float _lunartimer = 0;
    public float TimerMoon
    {
        get {
            //Debug.Log("Fetching Timer Moon: " + _lunartimer);
            return _lunartimer;
        }
        set
        {
            //Debug.Log("TimerMoon Being set to " + value);
            if (value < 0) { _lunartimer = 0; }
            else if (value > LunarMonthTime)
            {
                TimerMoon = value - LunarMonthTime;
            }
            else { _lunartimer = value; }
        }
    }

    // Starts with the full moon;
    /* Waning:    0.0-0.25
     * New:  0.26-0.5
     * Waxing:     0.51-0.75
     * Full:  0.76-0.99
     */
    public LunarPhase CurrentPhase { get {
            float percentage = TimerMoon / LunarMonthTime;
            LunarPhase lp = LunarPhase.Waning;
            if(percentage > 0.5)
            {
                if( percentage > 0.75) { lp = LunarPhase.Full; }
                else { lp = LunarPhase.Waxing; }

            }
            else if(percentage > 0.25)
            {
                lp = LunarPhase.New;
            }
            return lp;
                }
    }

    short _currentPlayerTurn = 0;
    public short CurrentPlayerTurn {
        get{ return _currentPlayerTurn; }
        set { if (value >= Player.Count || value < 0) {
                _currentPlayerTurn = 0;
                NotifyaPlayerOfTurnStart(Player[_currentPlayerTurn]);
            }
            else {
                Debug.Log("[GmM] Setting current Player to " + value);
                _currentPlayerTurn = value;
                NotifyaPlayerOfTurnStart(Player[_currentPlayerTurn]);
            } }
    }

    private void Start()
    {
        PlayerManager[] pms = GameObject.FindObjectsOfType<PlayerManager>();
        foreach(PlayerManager player in pms)
        {
            Player.Add(player);
            player.PregameSetup();
        }
        NotifyaPlayerOfTurnStart(Player[0]);
        Debug.Log("[-] Number of Players: " + Player.Count);
    }

    public void EndTurn(PlayerManager pm)
    {
        Debug.Log("[GmM] _currentPlayerTurn = " + _currentPlayerTurn);
        if (Player.Count > 1) { 
            if (pm == Player[_currentPlayerTurn])
            {
                CurrentPlayerTurn++;
                NotifyaPlayerOfTurnStart(Player[CurrentPlayerTurn]);
            }
        }
        else if(_currentPlayerTurn == 0)
        {
             _currentPlayerTurn = 1;
             _singlePlayertimer = 0.0f;
        }
    }

    private void Update()
    {
        /*
        if(Player.Count == 1)
        {
            _singlePlayertimer += Time.deltaTime;
            if(CurrentPlayerTurn == 1 && _singlePlayertimer > TimeBetweenTurns)
            {
                CurrentPlayerTurn = 0;
            }
        }
        */
        float dt = Time.deltaTime;
        //Debug.Log("Delta Time: " + dt);
        TimerDrawCard += dt;
        TimerMoon += dt;
        if (_gameWon && VictoryPanel)
        {
            if (VictoryPanel.color.a < 1)
            {
                Color newColor = new Color(VictoryPanel.color.r, VictoryPanel.color.g, VictoryPanel.color.b, VictoryPanel.color.a + (Time.deltaTime / 3));
                VictoryPanel.color = newColor;
            }
        }
    }

    private void NotifyaPlayerOfTurnStart(PlayerManager pm)
    {
        Debug.Log("[GmM]Telling Player " + pm.name + " to start their turn.");
        pm.StartTurn();
    }

    public void WinTheGame()
    {
        Debug.Log("You Win!");
        _gameWon = true;
    }
}
