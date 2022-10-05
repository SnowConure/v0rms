using Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    //[HideInInspector]
    public List<Worm> myWorms = new List<Worm>();

    public Action PlayerDied;

    public GameObject WormPrefab;
    public int playerID = 0;

    public bool IsAlive = true;

    private bool myTurn = false;

    

    public GameObject myModels;
    private void OnEnable()
    {
        TurnHandler.Instance.StartTurn += NewTurn;
        //myWorms.Clear();
    }

    private void OnDisable()
    {
        TurnHandler.Instance.StartTurn -= NewTurn;

    }

    void NewTurn(int _turn)
    {
        // Check if its not my turn
        if(_turn != playerID)
        {
            myTurn = false;
            // deactivate the Worms
            foreach (Worm worm in myWorms)
            {
                worm.Activate(false);
            }
            return;
        }

       // Ready the Worms
       foreach (Worm worm in myWorms)
       {
            worm.NewTurn();
       }
        myTurn = true;
         myWorms[0].Activate(true);
    }

    public void SpawnWorms(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Worm _newWorm = Instantiate(WormPrefab, transform.position + (Vector3.right * i), Quaternion.identity).GetComponent<Worm>();
            _newWorm.modelPrefab = myModels;
            _newWorm.GotClicked += ActivateWorm;
            _newWorm.Died += WormDied;
            myWorms.Add(_newWorm);

        }
    }

    void WormDied(Worm worm)
    {
        myWorms.Remove(worm);

        if(myWorms.Count == 0)
        {
            // Player Lost
            IsAlive = false;

            PlayerDied?.Invoke();
        }

        if(myTurn && IsAlive)
        myWorms[0].Activate(true);
    }

    void ActivateWorm(Worm _worm)
    {
        //if its my turn
        if (TurnHandler.Instance.playerTurn != playerID) return;

        // Ready the Worms
        foreach (Worm worm in myWorms)
        {
            worm.Activate(false);
        }

        _worm.Activate(true);
    
    }

    public void Dance()
    {
        Debug.Log("player" + myModels.name + myWorms.Count);



        // Ready the Worms
        foreach (Worm worm in myWorms)
        {
            worm.Dance();
        }
    }
}
