﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CardVersion2 
{
    public int cardID;
    public string cardName;
    public string cardDetail;
    public string cardType;

    public int cardCoinCost;
    public int cardAttack;
    public int cardHealth;

    public Sprite cardImage;

    public CardVersion2()
    {

    }

    public CardVersion2(int ID, string Name, string Detail, string Type, int Cost, int Attack, int Health, Sprite Image)
    {
        cardID = ID;
        cardName = Name;
        cardDetail = Detail;
        cardType = Type;
        cardCoinCost = Cost;
        cardAttack = Attack;
        cardHealth = Health;

        cardImage = Image;
    }
}