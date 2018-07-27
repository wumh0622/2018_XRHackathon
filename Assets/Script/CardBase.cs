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
    private CardManager.CardName cardName;
    private Image cardImage;
    private Text description;

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
        cardImage.sprite = _data.cardImage;
        description.text = _data.description;
    }

    //執行功能
    public void StartFunction()
    {
        Function1();
    }
    
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F2))
        {
            StartFunction();
        }
    }
}
