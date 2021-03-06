﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combat_maneger : MonoBehaviour
{
    public TurnBaseScript TBS;

    public List<GameObject> attack= new List<GameObject>();
    public List<GameObject> deffendingCardsRef = new List<GameObject>();

    public GameObject[] defend = new GameObject[5];
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
//        Debug.Log("I am working");

        for (int i = 0; attack.Count > i; i++)
        {
            //card defending card blocks attack from attacking card of the same position
            if (defend[i] != null)
            {
                string Decription = defend[i].GetComponent<CardDisplay>().card.description;
                string[] b = Decription.Split(' ');
                bool doge=false;
                for (int a = 0; b.Length > a; a++)
                {
                    if (b[a] == "Doge")
                    {
                        int chance = Random.Range(1, 10);
                        if (chance >= int.Parse(b[a+1])/10)
                            doge = true;
                    }
                }
                if (doge == false) {
                    int newHealth = defend[i].GetComponent<CardDisplay>().card.health - attack[i].GetComponent<CardDisplay>().card.attack;
                    defend[i].GetComponent<CardDisplay>().card.health = newHealth;
                    if (newHealth < 0)
                    {
                        string Decriptionx = attack[i].GetComponent<CardDisplay>().card.description;
                        string[] x = Decriptionx.Split(' ');
                        for (int y = 0; x.Length > y;y++)
                        {
                            if (x[i] == "Swarm")
                            {
                                if (TBS.playerTurn == false)
                                {
                                    TBS.player1Health += newHealth;
                                    TBS.player1HealthText.text = TBS.player1Health.ToString();
                                }
                                else
                                {
                                    TBS.player2Health += newHealth;
                                    TBS.player2HealthText.text = TBS.player2Health.ToString();
                                }

                            }
                        }
                    }
                    newHealth = attack[i].GetComponent<CardDisplay>().card.health - defend[i].GetComponent<CardDisplay>().card.attack;
                    attack[i].GetComponent<CardDisplay>().card.health = newHealth;
                }

            }
            else//if there isnt anything blocking attacking card direclyattack player
            {
                if (TBS.playerTurn == false)
                {
                    TBS.player1Health -= attack[i].GetComponent<CardDisplay>().card.attack;
                    TBS.player1HealthText.text = TBS.player1Health.ToString();
                }
                else
                {
                    TBS.player2Health -= attack[i].GetComponent<CardDisplay>().card.attack;
                    TBS.player2HealthText.text = TBS.player2Health.ToString();
                }
            }

            yield return new WaitForSeconds(1);
        }
        yield return new WaitForFixedUpdate();
        //clears list
        for(int i=0;attack.Count>i; i++)
        {
            if(attack[i]!=null)
                attack[i].GetComponent<CardDisplay>().attack_defend = 0;
        }
        attack.Clear();


        for (int i = 0; 5 > i; i++)
        {
            if (defend[i] != null)
            {
                defend[i].GetComponent<CardDisplay>().attack_defend = 0;
                defend[i] = null;
            }

        }

        //changes state to stop combatphose
        TBS.state = TurnBaseScript.TurnState.Nothing;
        //bool tick to stop calling of the combatphase
        started_combat = false;
        if (TBS.playerTurn == false)
        {
            TBS.buttons[2].gameObject.SetActive(false);
            TBS.buttons[0].gameObject.SetActive(false);
            TBS.buttons[1].gameObject.SetActive(false);
            TBS.buttons[3].gameObject.SetActive(false);
        }
        else
        {
            TBS.buttons[2].gameObject.SetActive(false);
            TBS.buttons[0].gameObject.SetActive(true);
            TBS.buttons[1].gameObject.SetActive(false);
            TBS.buttons[3].gameObject.SetActive(false);
        }
        print("x");
        if (TBS.playerTurn == false)
        {
            print("switch");
            yield return new WaitForSeconds(1);
            TBS.EndPlayerTurn();
        }
    }
}
