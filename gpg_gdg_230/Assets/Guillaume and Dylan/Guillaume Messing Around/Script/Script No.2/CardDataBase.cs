using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    public static List<CardVersion2> cardList = new List<CardVersion2>();

    private void Awake()
    {
        cardList.Add(new CardVersion2(0, "Trooper Token", "I dont exist in the deck", "Soldier", 1, 3, 3, Resources.Load <Sprite>("0"), 0, 0, 0, 0, 0, false, 0 , 0));
        cardList.Add(new CardVersion2(1, "Sniper Trooper", "On play: Deal 1 damage", "Soldier", 2, 1, 1, Resources.Load <Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(2, "Trooper", "They've been trained to kill", "Soldier", 3, 3, 3, Resources.Load <Sprite>("2"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(3, "Shield Trooper", "Defender", "Soldier", 2, 1, 3, Resources.Load<Sprite>("3"), 0, 0, 0, 0, 0, true, 0, 0));
        cardList.Add(new CardVersion2(4, "Sword Trooper", "On play: give other cards +1/+0", "Soldier", 3, 3, 2, Resources.Load <Sprite>("3"), 0, 0, 0, 0, 0, false, 1, 0));
        cardList.Add(new CardVersion2(5, "Support Trooper", "On play: this card gains +0/+1", "Soldier", 4, 2, 3, Resources.Load <Sprite>("4"), 0, 0, 0, 1, 0, false, 0, 0));
        cardList.Add(new CardVersion2(6, "The Commander of Trooper", "On play: summon x2 Trooper", "Soldier", 5, 4, 4, Resources.Load <Sprite>("5"), 0, 0, 0, 0, 2, false, 0, 0));
        cardList.Add(new CardVersion2(7, "The Genral of Trooper", "On play: summon x1 Trooper and give other Soldier +1/+1", "Soldier", 5, 4, 3, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 1, false, 1, 1));
        cardList.Add(new CardVersion2(8, "Frost Trooper", "On play: Stun 1 enemy unit", "Soldier", 3, 3, 2, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(9, "Air Trooper", "On play: draw 2 cards", "Soldier", 4, 4, 2, Resources.Load<Sprite>("6"), 2, 0, 0, 0, 0, false, 0, 0));

        cardList.Add(new CardVersion2(10, "Stealth Trooper", "Summon 3 Monster", "French", 1, 1, 1, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(11, "Electric Trooper", "Summon 3 Monster", "French", 1, 1, 1, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(12, "Burner Trooper", "Draw 1 Card", "French", 4, 2, 5, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(13, "The Leader", "Add 1 Max Mana", "French", 5, 4, 4, Resources.Load<Sprite>("2"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(14, "Buff Trooper", "Buff 3 ATK and 3 DEF", "French", 1, 2, 1, Resources.Load<Sprite>("3"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(15, "Elite Trooper", "Buff 1 ATK", "French", 0, 1, 1, Resources.Load<Sprite>("4"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(16, "Rogue Trooper", "Buff 3 Defence", "French", 7, 8, 8, Resources.Load<Sprite>("5"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(17, "The Leader's Apprentice", "Summon 3 Monster", "French", 1, 1, 1, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(18, "Security Trooper", "Summon 3 Monster", "French", 1, 1, 1, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(19, "Assassin Trooper", "Summon 3 Monster", "French", 1, 1, 1, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(20, "Super Trooper", "Summon 3 Monster", "French", 1, 1, 1, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(21, "Storm in Trooper", "Summon 3 Monster", "French", 1, 1, 1, Resources.Load<Sprite>("6"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(22, "Training Camp", "Draw 1 Card", "French", 4, 2, 5, Resources.Load<Sprite>("1"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(23, "Call for reinforcement", "Add 1 Max Mana", "French", 5, 4, 4, Resources.Load<Sprite>("2"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(24, "We need to prepare", "Buff 3 ATK and 3 DEF", "French", 1, 2, 1, Resources.Load<Sprite>("3"), 0, 0, 0, 0, 0, false, 0, 0));
        cardList.Add(new CardVersion2(25, "Uprgade", "Buff 1 ATK", "French", 0, 1, 1, Resources.Load<Sprite>("4"), 0, 0, 0, 0, 0, false, 0, 0));
    }
}
