﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThisCard : MonoBehaviour
{
    //This is for the card deatils in our game.
    public List<CardVersion2> thisCard = new List<CardVersion2>();
    public int thisID;

    public int id;
    public string thisCardName;
    public string thisCardDetails;
    public string thisCardType;
    public int thisCardCost;
    public int thisCardAttack;
    public int thisCardHealth;


    public Text nameText;
    public Text deatilText;
    public Text typeText;
    public Text costText;
    public Text attackText;
    public Text healthText;

    public Sprite thisCardSprite;
    public Image cardImage;

    public bool cardBack;
    //public static bool staticCardBack;

    //This is to make sure the card go to the hand.
    public GameObject hand;

    public int numberOfCardsInDeck;

    //Seting up the summoning mechanic
    public bool canBeSummon;
    public bool summoned;
    public GameObject battleZone;

    //This is to be use for our abilities.
    //Need to make a summoning one.
    public static int drawX;
    public int drawXCards;
    public int addXMaxCoin;
    public int buffXATK;
    public int buffXHealth;
    public int summoningMonsters;
    public bool token = true;
    public List<CardVersion2> tokenCards = new List<CardVersion2>();

    //These are forbeing able to attack or not
    // and which one to attack.
    public GameObject ableToAttackObject;
    public GameObject target;
    public GameObject enemy;

    public bool summoningSickness;
    public bool cantAttack;
    public bool canAttack;

    public static bool staticTargeting;
    public static bool staticTargetingEnemy;

    public bool targeting;
    public bool targetingEnemy;

    public bool onlyThisCardAttack;

    //Thi is to make the player know when they are able to summon/use their card.
    public GameObject ableToUseCard;

    // Start is called before the first frame update
    void Start()
    {
        thisCard[0] = CardDataBase.cardList[thisID];
        numberOfCardsInDeck = PlayerDeck.deckSize;

        canBeSummon = false;
        summoned = false;

        drawX = 0;

        canAttack = false;
        summoningSickness = true;

        enemy = GameObject.Find("EnemyHealth");

        targeting = false;
        targetingEnemy = false;
    }

    // Update is called once per frame
    void Update()
    {
        hand = GameObject.Find("PlayerHand Version2");
        if (this.transform.parent == hand.transform.parent)
        {
            cardBack = false;
        }

        if (summoned == false)
        {
            id = thisCard[0].cardID;
            thisCardName = thisCard[0].cardName;
            thisCardDetails = thisCard[0].cardDetail;
            thisCardType = thisCard[0].cardType;
            thisCardCost = thisCard[0].cardCoinCost;
            thisCardAttack = thisCard[0].cardAttack;
            thisCardHealth = thisCard[0].cardHealth;
            thisCardSprite = thisCard[0].cardImage;
        }

        drawXCards = thisCard[0].drawXCards;
        addXMaxCoin = thisCard[0].addXMaxCoin;
        buffXATK = thisCard[0].buffATK;
        buffXHealth = thisCard[0].buffHealth;
        summoningMonsters = thisCard[0].summonMonster;

        nameText.text = "" + thisCardName;
        deatilText.text = "" + thisCardDetails;
        typeText.text = "" + thisCardType;
        costText.text = "" + thisCardCost;
        attackText.text = "" + thisCardAttack;
        healthText.text = "" + thisCardHealth;
        cardImage.sprite = thisCardSprite;

        //staticCardBack = cardBack;

        if (this.tag == "Clone")
        {
            thisCard[0] = PlayerDeck.staticDeck[numberOfCardsInDeck - 1];
            numberOfCardsInDeck -= 1;
            PlayerDeck.deckSize -= 1;
            cardBack = false;
            this.tag = "Untagged";
        }

        if (TurnSystem.currentCoin >= thisCardCost && summoned == false && TurnSystem.isYourTurn == true)
            canBeSummon = true;

        else
            canBeSummon = false;


        if (canBeSummon == true)
            gameObject.GetComponent<DraggableCard>().enabled = true;

        else
            gameObject.GetComponent<DraggableCard>().enabled = false;


        battleZone = GameObject.Find("Field");


        if (summoned == false && this.transform.parent == battleZone.transform)
            Summon();

        if (canAttack == true)
        {
            ableToAttackObject.SetActive(true);
        }
        else
        {
            ableToAttackObject.SetActive(false);
        }

        if (TurnSystem.isYourTurn == false && summoned == true)
        {
            summoningSickness = false;
            cantAttack = false;
        }

        if (TurnSystem.isYourTurn == true && summoningSickness == false && cantAttack == false)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        targeting = staticTargeting;
        targetingEnemy = staticTargetingEnemy;

        if (targetingEnemy == true)
            target = enemy;
        else
            target = null;

        if (targeting == true && targetingEnemy == true && onlyThisCardAttack == true)
            Attack();

        if (canBeSummon == true && TurnSystem.isYourTurn == true)
            ableToUseCard.SetActive(true);
        else
            ableToUseCard.SetActive(false);

        if (summoningMonsters > 0 && token == true)
        {
            AddToken(summoningMonsters);
        }
    }

    public void AddToken(int x)
    {
        for (int i = 0; i < x; i++)
        {
            tokenCards[i] = CardDataBase.cardList[0];
            token = false;
        }
    }

    public void Summon()
    {
        TurnSystem.currentCoin -= thisCardCost;
        summoned = true;

        MaxCoin(addXMaxCoin);
        BuffAttack(buffXATK);
        BuffHealth(buffXHealth);
        drawX = drawXCards;
    }

    public void MaxCoin(int x)
    {
        TurnSystem.maxCoin += x;
        if (TurnSystem.maxCoin >= 10)
        {
            TurnSystem.maxCoin = 10;
        }
    }
    
    public void BuffAttack(int x)
    {
        thisCardAttack += x;
        Debug.Log("Buff Attack by " + x);
        attackText.text = thisCardAttack.ToString();
    }

    public void BuffHealth(int x)
    {
        thisCardHealth += x;
        Debug.Log("Buff health by " + x);
        healthText.text = "" + thisCardHealth;
    }

    public void SummoningTheMonster(int x)
    {
        for (int i = 0; i < x; i++)
        {

        }
    }

    public void Attack()
    {
        if (canAttack == true)
        {
            if (target != null)
            {
                if (target == enemy)
                {
                    EnemyHp.staticHp -= thisCardAttack;
                    targeting = false;
                    cantAttack = true;
                }

                if (target.name == "CardToHand(Clone)")
                {
                    canAttack = true;
                }
            }
        }
    }

    public void UntargetedEdEnemy()
    {
        staticTargetingEnemy = false;
    }

    public void TargetedEnemy()
    {
        staticTargetingEnemy = true;
    }

    public void StartAttack()
    {
        staticTargeting = true;
    }

    public void StopAttack()
    {
        staticTargeting = false;
    }

    public void OneCardAttack()
    {
        onlyThisCardAttack = true;
    }

    public void OneCardAttackStop()
    {
        onlyThisCardAttack = false;
    }


}
