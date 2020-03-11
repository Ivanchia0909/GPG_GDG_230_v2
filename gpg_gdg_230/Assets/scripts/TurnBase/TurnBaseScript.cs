﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * The main purpose of this sc res
 */

public class TurnBaseScript : MonoBehaviour
{
    //To make sure that the turn are played properly.
    public enum TurnState { StartTurn, PlayerTurn, Untap, CardPlayed, Response, Attack, End, Nothing, TimeWasted, EndofBattle  }
    public TurnState state = TurnState.Nothing;

    public Hand player1Hand;
    public Hand player2Hand;

    //The main players health base.
    public int player1Health = 20;
    public int player2Health = 20;

    public Text player1HealthText;
    public Text player2HealthText;

    //Making sure to see which player turn it is.
    public bool playerTurn = false;

    //The timer to make sure players don't spend too long on it.
    public int turnTimer = 30;
    private int reduceTime1 = 30;
    private int reduceTime2 = 30;
    private int player1AFKStrike;
    private int player2AFKStrike;

    //To prevent this from overlaping
    public bool timerIsOn = false;

    //To see which player goes first when the game starts.
    public int whoGoesFirst;

    public bool startOfTheGame = true;

    private int defaultGold;
    private int defaultMana1;
    private int defaultMana2;

    public Text player1CoinText;
    public Text player2CoinText;
    public Text player1ManaText;
    public Text player2ManaText;

    //Using so that there is a delay when it comes to see what it does.
    private int actiontime = 3;

    public GameObject attackButton;
    public GameObject[] buttons;

    public GameObject player_active_ui;

    public GameObject AI_active_ui;
    //added by milan
    public int turns = 0;
    // Start is called before the first frame update
    void Start()
    {
        player1HealthText.text = player1Health.ToString();
        player2HealthText.text = player2Health.ToString();

        whoGoesFirst = Random.Range(1, 10);
        if (whoGoesFirst <= 5)
            playerTurn = true;
        else
            playerTurn = false;

        state = TurnState.StartTurn;

    }

    #region Update
    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            //For when the turn starts for a player.
            case (TurnState.StartTurn):
                

                if (startOfTheGame == true)
                {
                    if (playerTurn == true)
                    {
                        player1Hand.active = true;
                        player2Hand.active = false;
                        player_active_ui.SetActive(true);
                        AI_active_ui.SetActive(false);
                    }
                    else
                    {
                        player1Hand.active = false;
                        player2Hand.active = true;
                        player_active_ui.SetActive(false);
                        AI_active_ui.SetActive(true);
                    }
                    Draw7();
                }
                else
                {
                    //need to make the player draw 1
                    if (playerTurn == true)
                    {
                        player1Hand.active = true;
                        player2Hand.active = false;
                        player1Hand.pickCard();
//                        Debug.Log("Player 1 drew a card");
                    }
                    else
                    {
                        player1Hand.active = false;
                        player2Hand.active = true;
                        player2Hand.pickCard();
//                        Debug.Log("Player 2 drew a card");
                    }
                    if (playerTurn == true)
                    {
                        player_active_ui.SetActive(true);
                        AI_active_ui.SetActive(false);
                        if (turns >= 2)
                            player1Hand.MonsterSicknessIsOver();

                    }
                    else
                    {
                        player_active_ui.SetActive(false);
                        AI_active_ui.SetActive(true);
                        if (turns >= 2)
                            player2Hand.MonsterSicknessIsOver();

                    }
                }
                GainManaAndCoin();
                state = TurnState.Untap;
                break;
            case (TurnState.Untap):
                UntapCard();
               // state = TurnBaseScript.TurnState.PlayerTurn;

                //-------------------------------------------------------------------------------------------added stuff here
                // player2Hand.MonsterSicknessIsOver();
                // player1Hand.MonsterSicknessIsOver();

                break;
            case (TurnState.PlayerTurn):
                if (playerTurn == true)
                {
                    buttons[2].gameObject.SetActive(true);
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(false);
                }
                else
                {
                    buttons[2].gameObject.SetActive(false);
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(false);
                }
                //Making the turn timer, we need some more work on.
                if (timerIsOn == false)
                {
                    StartCoroutine("CountDown");
                    timerIsOn = true;
                }
                if(turnTimer <= 0)
                {
                    StopCoroutine("CountDown");
                    timerIsOn = false;

                    state = TurnState.TimeWasted;
                }
                //attackButton.SetActive(true);
                


                break;
            case (TurnState.Attack):
                if (playerTurn == true)
                {
                    buttons[2].gameObject.SetActive(false);
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(true);
                }
                else
                {
                    buttons[2].gameObject.SetActive(false);
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(false);
                }
                break;

            case (TurnState.Response):
                if (playerTurn == false)
                {
                    buttons[2].gameObject.SetActive(false);
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(true);
                    buttons[3].gameObject.SetActive(false);
                }
                else
                {
                    buttons[2].gameObject.SetActive(false);
                    buttons[0].gameObject.SetActive(false);
                    buttons[1].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(false);
                }
                PlayerResponeToAction();

                break;

            case (TurnState.End):
                turns += 1;
                turnTimer = 30;

                if (playerTurn == true)
                {
                    player1AFKStrike = 0;
                    reduceTime1 = 30;
                    
                    playerTurn = false;
                    
                }
                else
                {
                    player2AFKStrike = 0;
                    reduceTime2 = 30;
                   
                    playerTurn = true;
                    
                }
         //       Debug.Log("Player 2 Health is " + player2Health);
                state = TurnState.StartTurn;
                break;

            case (TurnState.Nothing):

                break;

            case (TurnState.TimeWasted):
                if (playerTurn == true)
                    player1AFKStrike += 1;
                else
                    player2AFKStrike += 1;
                TimerEndTurn();
                break;

        }

        if (player1Health <= 0 || player2Health <= 0)
        {
            if (state != TurnState.Nothing)
            {
                GameIsOver();
            }
        }

        if (actiontime <= 0)
        {
            StopCoroutine("ActionCountDown");
            actiontime = 3;
            state = TurnState.PlayerTurn;
        }

        player1HealthText.text = player1Health.ToString();
        player2HealthText.text = player2Health.ToString();
    }
    #endregion

    void Draw7()
    {
        player2Hand.pick7();
        player1Hand.pick7();
        startOfTheGame = false;
    }

    //This is to so that each player will gain Gold each turn while only gain Mana at the start of the player's turn.
    void GainManaAndCoin()
    {
        player1Hand.playerGold = defaultGold;
        player2Hand.playerGold = defaultGold;

        player1Hand.playerGold += 1;
        player2Hand.playerGold += 1;
        defaultGold += 1;

        player1Hand.playerMana = defaultMana1;
        player2Hand.playerMana = defaultMana2;

        if (playerTurn == true)
        {
            player1Hand.playerMana += 1;
            defaultMana1 += 1;
        }
        else
        {
            player2Hand.playerMana += 1;
            defaultMana2 += 1;
        }

        if (player1Hand.playerGold >= 10)
            player1Hand.playerGold = 10;
        if (player2Hand.playerGold >= 10)
            player2Hand.playerGold = 10;
        if (player1Hand.playerMana >= 5)
            player1Hand.playerMana = 5;
        if (player2Hand.playerMana >= 5)
            player2Hand.playerMana = 5;

        player1CoinText.text = player1Hand.playerGold.ToString();
        player1ManaText.text = player1Hand.playerMana.ToString();
        player2CoinText.text = player2Hand.playerGold.ToString();
        player2ManaText.text = player2Hand.playerMana.ToString();

    }

    void PlayerResponeToAction()
    {
        turnTimer = 30;
    }

    public void ResponeToAttacking()
    {
        state = TurnState.Response;
    }

    public void GoingForAttack()
    {
        state = TurnState.Attack;
    }

    public void Battle()
    {
        state = TurnState.EndofBattle;
    }

    public void EndPlayerTurn()
    {
        state = TurnState.End;
    }

    //This is use for when the player is AFK 
    //If there are AFK for too long, then they will lose.
    public void TimerEndTurn()
    {
        if (player1AFKStrike == 3)
            GameIsOver();
        if (player2AFKStrike == 3)
            GameIsOver(); 

        if (state == TurnState.TimeWasted)
        {
            turnTimer = 30;
            if (playerTurn == true)
            {
                playerTurn = false;
                if (player2AFKStrike >= 1)
                {
                    reduceTime2 = reduceTime2 / 2;
                    turnTimer = reduceTime2;
                }
                state = TurnState.StartTurn;
            }
            else if (playerTurn == false)
            {
                playerTurn = true;
                if (player1AFKStrike >= 1)
                {
                    reduceTime1 = reduceTime1 / 2;
                    turnTimer = reduceTime1;
                }
                state = TurnState.StartTurn;
            }
        }

    }

    //Once the game is done.
    public void GameIsOver()
    {
        if (player1AFKStrike == 3)
        {
            Debug.Log("Player 1 lose");
            for (int i = 0; buttons.Length > i; i++)
            {
                buttons[i].SetActive(false);
            }
        }
        if (player2AFKStrike == 3)
        {
            Debug.Log("Player 2  lose");
            for (int i = 0; buttons.Length > i; i++)
            {
                buttons[i].SetActive(false);
            }
        }

        if (player1Health <= 0)
        {
            Debug.Log("Player 1 Lose");
            for (int i = 0; buttons.Length > i; i++)
            {
                buttons[i].SetActive(false);
            }
        }
        if (player2Health <= 0)
        {
            Debug.Log("Player 2 Lose");
            for (int i = 0; buttons.Length > i; i++)
            {
                buttons[i].SetActive(false);
            }
        }

        state = TurnState.Nothing;
    }

    //This is so that the player doesn't take too long
    IEnumerator CountDown()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            turnTimer--;
        }

    }
    IEnumerator ActionCountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            actiontime--;
        }

    }

    public void ReadTheCard(ScriptableCard card)
    {
        StopCoroutine("CountDown");
        timerIsOn = false;
        StartCoroutine("ActionCountDown");

//        print(card.description.ToString());

        player1CoinText.text = player1Hand.playerGold.ToString();
        player1ManaText.text = player1Hand.playerMana.ToString();
        player2CoinText.text = player2Hand.playerGold.ToString();
        player2ManaText.text = player2Hand.playerMana.ToString();
    }

    public void UntapCard()
    {
        if (playerTurn == true)
            player1Hand.UntapTheCards();
        else
            player2Hand.UntapTheCards();
    }

    public void HasAttack()
    {
        player1Hand.firstAttack = false;
    }

}
