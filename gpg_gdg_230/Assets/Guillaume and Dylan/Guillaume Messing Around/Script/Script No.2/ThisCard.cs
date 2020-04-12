using System.Collections;
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
    public GameObject tokenObject;
    public List<CardVersion2> tokenCards = new List<CardVersion2>();
    public int buffOtherCardsATK;
    public static int staticBuffOtherCardsATK;
    public int buffOtherCardsHealth;
    public static int staticBuffOtherCardsHealth;

    public BuffingOtherCardsScript buffingOthersCards;

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

    public CardsOnTheField field;
    public GameObject fieldObject;
    public GameObject cardObject;



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

        fieldObject = GameObject.Find("Field");
        field = fieldObject.GetComponent<CardsOnTheField>();

    }

    // Update is called once per frame
    void Update()
    {
        hand = GameObject.Find("PlayerHand Version2");
        if (this.transform.parent == hand.transform.parent)
        {
            cardBack = false;
        }

        id = thisCard[0].cardID;
        thisCardName = thisCard[0].cardName;
        thisCardDetails = thisCard[0].cardDetail;
        thisCardType = thisCard[0].cardType;
        thisCardCost = thisCard[0].cardCoinCost;
        thisCardSprite = thisCard[0].cardImage;

        if (summoned == false)
        {
            thisCardAttack = thisCard[0].cardAttack;
            thisCardHealth = thisCard[0].cardHealth;
        }

        drawXCards = thisCard[0].drawXCards;
        addXMaxCoin = thisCard[0].addXMaxCoin;
        buffXATK = thisCard[0].buffATK;
        buffXHealth = thisCard[0].buffHealth;
        summoningMonsters = thisCard[0].summonMonster;
        buffOtherCardsATK = thisCard[0].buffOtherATK;
        buffOtherCardsHealth = thisCard[0].buffOtherHealth;

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

        /*
        //Changing the tag of Token in order to make it be able to summon it to the field.
        //Instead of it being summoned to the hand.
        if (thisCard[0].cardID == 0)
        {
            this.tag = "Token";
        }
        else
            this.tag = thisCard[0].cardType;
            */

        //So right here it make sures that all the Requirements for this game to summon are in the perfect.
        //While also making sure that I dont flood the field.
        if (TurnSystem.currentCoin >= thisCardCost && summoned == false && TurnSystem.isYourTurn == true && field.fieldCards.Count < 5)
            canBeSummon = true;

        else 
            canBeSummon = false;


        if (canBeSummon == true)
            gameObject.GetComponent<DraggableCard>().enabled = true;

        else
            gameObject.GetComponent<DraggableCard>().enabled = false;


        battleZone = GameObject.Find("Field");
        
        if (summoned == false && this.transform.parent == battleZone.transform && field.fieldCards.Count < 5)
            Summon();

        //This is the setup for the battle System.
        //I still need to fix it where the player chose to attack and the opponent must who to defend with.
        if (canAttack == true)
            ableToAttackObject.SetActive(true);
        else
            ableToAttackObject.SetActive(false);

        if (TurnSystem.isYourTurn == false && summoned == true)
        {
            summoningSickness = false;
            cantAttack = false;
        }

        if (TurnSystem.isYourTurn == true && summoningSickness == false && cantAttack == false)
            canAttack = true;
        else
            canAttack = false;

        targeting = staticTargeting;
        targetingEnemy = staticTargetingEnemy;

        if (targetingEnemy == true)
            target = enemy;
        else
            target = null;

        if (targeting == true && targetingEnemy == true && onlyThisCardAttack == true)
            Attack();

        //Making it visable for the Player to know if there able to summon or not.
        if (canBeSummon == true && TurnSystem.isYourTurn == true)
            ableToUseCard.SetActive(true);
        else
            ableToUseCard.SetActive(false);

        //Making a list for the token Ability.
        if (summoningMonsters > 0 && token == true)
            AddToken(summoningMonsters);
        if (tokenCards == null)
            return;

        if (thisCardHealth <= 0)
        {
            Invoke("DestroyMonster", 1.5f);
        }

    }

    public void AddToken(int x)
    {
        for (int i = 0; i < x; i++)
        {
            tokenCards.Add(CardDataBase.cardList[0]);
        }
        token = false;
    }

    public void Summon()
    {
        CardsOnTheField.monsterAttack = thisCardAttack;
        Debug.Log("summoning");
        TurnSystem.currentCoin -= thisCardCost;
        summoned = true;
        this.tag = thisCard[0].cardType;
        field.fieldCards.Add(cardObject);
        AddToken(summoningMonsters);

        MaxCoin(addXMaxCoin);
        BuffAttack(buffXATK);
        BuffHealth(buffXHealth);
        SummoningTheMonster(summoningMonsters);
        drawX = drawXCards;
        CardsOnTheField.beingSummoned = true;

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
        if (buffOtherCardsATK > 0)
        {
            buffingOthersCards.buffingOtherCardsATKBool = true;
            buffingOthersCards.attackBuff = buffOtherCardsATK;
        }

        if (buffingOthersCards.buffingOtherCardsATKBool == false)
        {
            thisCardAttack += x;
            attackText.text = thisCardAttack.ToString();
        }
    }

    public void BuffHealth(int x)
    {
        thisCardHealth += x;
        healthText.text = "" + thisCardHealth;
    }

    public void SummoningTheMonster(int x)
    {
        for (int i = 0; i < tokenCards.Count; i++)
        {
            if (field.fieldCards.Count >= 5)
            {
                break;
            }
            else if (field.fieldCards.Count < 5)
            {
                Debug.Log("creating card: " + tokenObject);
                Instantiate(tokenObject);
                tokenCards.Remove(CardDataBase.cardList[0]);
                //if (!field.fieldCards.Contains(tokenObject))
                    //field.fieldCards.Add(tokenObject);
            }
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

    public void DestroyMonster()
    {
        field.fieldCards.Remove(cardObject);
        Destroy(gameObject);
    }
}
