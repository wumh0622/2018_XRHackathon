using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CardBase : MonoBehaviour
{
    private CardManager cardManager;
    private CardManager Card_Manager { get { if (cardManager == null) cardManager = CardManager.instance; return cardManager; } }

    public CardManager.CardSpecies cardSpecies;
    public CardManager.CardName cardName;
    private Image cardImage;
    private Text description;
    private int money;

    private Action function;
    public Action Function1
    {
        get
        {
            if (function == null)
                function= Card_Manager.GetCardFunction(cardName);

            return function;
        }
    }

    public void SetCardData(CardManager.CardData _data)
    {
        cardSpecies = _data.cardSpecies;
        cardName = _data.cardName;
        if (cardImage != null)
            cardImage.sprite = _data.cardImage;
        if (description != null)
            description.text = _data.description;
        money = _data.needMoney;
    }

    //執行功能
    public void StartFunction()
    {
        Function1();
    }

    public void BuyThisCard()
    {
        //判斷是否有錢買
        //扣除money
        Card_Manager.BuyOneNewCard(cardName);
        Destroy(gameObject);
    }
}
