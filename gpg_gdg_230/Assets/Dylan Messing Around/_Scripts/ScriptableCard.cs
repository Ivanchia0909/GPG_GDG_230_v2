﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Creatures")]
public class ScriptableCard : ScriptableObject
{
    public int ID;
    public new string name;
    public string description;

    public Sprite artwork;

    public int manaCost;
    public int attack;
    public int health;
    //This is so that when it is played on the field for the first time
    // it won't be allow to attack.
    public bool monsterSickness = true;
}

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Spells")]
public class ScriptableSpellCard : ScriptableObject
{
    public int ID;
    public new string name;
    public string description;

    public Sprite artwork;

    public int manaCost;
    public int attack;
}

