﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combat_maneger : MonoBehaviour
{
    public TurnBaseScript TBS;

    public List<GameObject> attack= new List<GameObject>();
    public List<GameObject> defend = new List<GameObject>();
    public List<GameObject> DelayedRemoval = new List<GameObject>();

    public bool started_combat=false;

     
    // Start is called before the first frame update
    void Start()
    {
        TBS = GameObject.Find("maneger object").GetComponent<TurnBaseScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TBS.state == TurnBaseScript.TurnState.EndofBattle && started_combat == false)
        {
            StartCoroutine(CombatPhase());
            started_combat = true;
        }

    }
    //this alows cards in the attack and defend list to interact
    IEnumerator CombatPhase()
    {
        Debug.Log("I am working");

        for (int i = 0; attack.Count > i; i++)
        {
            //card defending card blocks attack from attacking card of the same position
            if (defend.Count > i && defend.Count != 0 && defend[i] != null)
            {
                int newHealth = defend[i].GetComponent<CardDisplay>().card.health - attack[i].GetComponent<CardDisplay>().card.attack;
                defend[i].GetComponent<CardDisplay>().card.health = newHealth;


                newHealth = attack[i].GetComponent<CardDisplay>().card.health - defend[i].GetComponent<CardDisplay>().card.attack;
                attack[i].GetComponent<CardDisplay>().card.health = newHealth;


            }
            else//if there isnt anything blocking attacking card direclyattack player
            {
                if (TBS.playerTurn == false)
                {
                    TBS.player1Health -= attack[i].GetComponent<CardDisplay>().card.attack;
                }
                else
                {
                    TBS.player2Health -= attack[i].GetComponent<CardDisplay>().card.attack;
                }
            }

            yield return new WaitForSeconds(1);
        }
        yield return new WaitForFixedUpdate();
        //clears list
        attack.Clear();
        for(int i=0; DelayedRemoval.Count > i; i++)
        {
            defend.Remove(DelayedRemoval[i]);
        }
        DelayedRemoval.Clear();
        defend.Clear();
        //changes state to stop combatphose
        TBS.state = TurnBaseScript.TurnState.Nothing;
        //bool tick to stop calling og the combatphose
        started_combat = false;
    }
}
