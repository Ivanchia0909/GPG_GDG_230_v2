using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffingOtherCardsScript : MonoBehaviour
{
    public int attackBuff;

    public int healthBuff;

    public bool buffingOtherCardsATKBool = false;

    public bool buffingOtherCardsHealthBool = false;

    public ThisCard thisCard;

    // Update is called once per frame
    void Update()
    {
        if (buffingOtherCardsATKBool == true || buffingOtherCardsHealthBool == true)
            BuffingTheCardVoid();
    }

    public void BuffingTheCardVoid()
    {
        if (buffingOtherCardsATKBool == true)
        {
            Debug.Log("Hello my name is edler Maguex");
            thisCard.thisCardHealth += attackBuff;
            attackBuff = 0;
            buffingOtherCardsATKBool = false;
        }
    }
}
