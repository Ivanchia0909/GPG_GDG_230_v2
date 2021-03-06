﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This card purpose is to make sure that the cards you draw go into hand
// while having any tokens that are being played to the field.
public class CardToHand : MonoBehaviour
{
    public GameObject hand;
    public GameObject cardObject;
    public CardsOnTheField field;
    public GameObject fieldObject;

    // Start is called before the first frame update
    void Start()
    {
        hand = GameObject.Find("PlayerHand Version2");
        cardObject.transform.SetParent(hand.transform);
        cardObject.transform.localScale = Vector3.one;
        cardObject.transform.position = new Vector3(transform.position.x, transform.position.y, -48);
        cardObject.transform.eulerAngles = new Vector3(25, 0, 0);
        fieldObject = GameObject.Find("Field");
        field = fieldObject.GetComponent<CardsOnTheField>();

    }

}
