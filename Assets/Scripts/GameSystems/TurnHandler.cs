using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    public Action<int> StartTurn;

    public int playerTurn = 0;


    #region Singleton
    public static TurnHandler Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
        DontDestroyOnLoad(this);

    }
    #endregion

    public void EndTurn()
    {
    }
    public void NextTurn()
    {
        StartTurn?.Invoke(GetTurn());
    }

    int GetTurn()
    {
        for (int i = 0; i < GlobalGame.Instance.NumOfPlayers; i++)
        {
            playerTurn++;
            if (playerTurn > GlobalGame.Instance.NumOfPlayers)
            {
                playerTurn = 0;
                return playerTurn;
            }

            if (GlobalGame.Instance.Players[playerTurn - 1].IsAlive == true)
                return playerTurn;

        }
        return 0;



    }
}

