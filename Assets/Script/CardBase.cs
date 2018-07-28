﻿using UnityEngine;
using UnityEngine.UI;
using System;

public class CardBase : MonoBehaviour
{
    private CardManager cardManager;
    private CardManager Card_Manager { get { if (cardManager == null) cardManager = CardManager.instance; return cardManager; } }
    private GameFlow gameFlow;
    private GameFlow Game_Flow { get { if (gameFlow == null) gameFlow = GameFlow.instance; return gameFlow; } }


    public CardManager.CardSpecies cardSpecies;
    public CardManager.CardName cardName;
    [SerializeField] Text nameText;
    [SerializeField] Image cardImage;
    [SerializeField] Text description;
    private int money;

    private Action function;
    /*public Action Function1
    {
        get
        {
            if (function == null)
                function= Card_Manager.GetCardFunction(cardName);

            return function;
        }
    }*/

    public void SetCardData(CardManager.CardData _data)
    {
        cardSpecies = _data.cardSpecies;
        cardName = _data.cardName;
        money = _data.needMoney;
        if (nameText != null)
            nameText.text = _data.cardNameText;
        if (cardImage != null)
            cardImage.sprite = _data.cardImage;
        if (description != null)
            description.text = _data.description;
    }

    public void MyisExitCard()
    {
        Shop.instance.CloseShopMenu();
    }

    //執行功能
    public void StartFunction()
    {
        //Function1();
    }

    public void BuyThisCard()
    {
        if (cardSpecies != CardManager.CardSpecies.Commodity)
        {
            Debug.Log("此物不是商品");
            if (cardName == CardManager.CardName.Exit)
            {
                MyisExitCard();
            }
            return;
        }

        if (!Game_Flow.DetuctMoney(money))
            Debug.Log("金錢不足");
        else
        {
            Card_Manager.BuyOneNewCard(cardName);
            Destroy(gameObject);
        }
    }

}
