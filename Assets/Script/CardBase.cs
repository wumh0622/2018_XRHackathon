using UnityEngine;
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
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    CardManager.CardName _c;
    void Update()
    {
        if (_c != cardName)
        {
            _c = cardName;
            Debug.Log(cardName);
        }
    }
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
            description.text = _data.description + _data.cardText;

        Debug.Log("SetCardData");
    }

    public void MyisExitCard()
    {
        Shop.instance.CloseShopMenu();
    }

    //執行功能
    public void StartFunction()
    {
        switch (cardName)
        {
            case CardManager.CardName.A1:
                GuestManager.instance.currentGuest.gameObject.GetComponent<Animator>().SetTrigger("Wind");
                break;
            case CardManager.CardName.A2:
                GuestManager.instance.currentGuest.gameObject.GetComponent<Animator>().SetTrigger("Wind");
                break;
            case CardManager.CardName.A3:
                GuestManager.instance.currentGuest.gameObject.GetComponent<Animator>().SetTrigger("Wind");
                break;

            case CardManager.CardName.B1:
                GuestManager.instance.currentGuest.gameObject.GetComponent<Animator>().SetInteger("Random", 2);
                GuestManager.instance.currentGuest.gameObject.GetComponent<Animator>().SetTrigger("Speek");
                break;
            case CardManager.CardName.B2:
                GuestManager.instance.currentGuest.gameObject.GetComponent<Animator>().SetTrigger("Wind");
                break;
            case CardManager.CardName.B3:
                GuestManager.instance.currentGuest.gameObject.GetComponent<Animator>().SetTrigger("Wind");
                break;

            case CardManager.CardName.C1:
                GuestManager.instance.currentGuest.gameObject.GetComponent<Animator>().SetTrigger("Sad");
                GameFlow.instance.heart--;
                GuestManager.instance.currentGuest.DecreseHeart();
                break;
            case CardManager.CardName.C2:
                GuestManager.instance.currentGuest.gameObject.GetComponent<Animator>().SetTrigger("Sad");
                GameFlow.instance.heart--;
                GuestManager.instance.currentGuest.DecreseHeart();
                break;
            case CardManager.CardName.C3:
                GuestManager.instance.currentGuest.gameObject.GetComponent<Animator>().SetInteger("Random", 2);
                GuestManager.instance.currentGuest.gameObject.GetComponent<Animator>().SetTrigger("Speek");
                GameFlow.instance.heart++;
                GuestManager.instance.currentGuest.CreseHeart();
                break;

            case CardManager.CardName.D1:
                GuestManager.instance.currentGuest.gameObject.GetComponent<Animator>().SetInteger("Random", 0);
                GuestManager.instance.currentGuest.gameObject.GetComponent<Animator>().SetTrigger("Speek");
                GameFlow.instance.heart++;
                GuestManager.instance.currentGuest.CreseHeart();
                break;
            case CardManager.CardName.D2:
                GuestManager.instance.currentGuest.gameObject.GetComponent<Animator>().SetInteger("Random", 1);
                GuestManager.instance.currentGuest.gameObject.GetComponent<Animator>().SetTrigger("Speek");
                GameFlow.instance.heart++;
                GuestManager.instance.currentGuest.CreseHeart();
                break;
            case CardManager.CardName.D3:
                GuestManager.instance.currentGuest.gameObject.GetComponent<Animator>().SetInteger("Random", 2);
                GuestManager.instance.currentGuest.gameObject.GetComponent<Animator>().SetTrigger("Speek");
                GameFlow.instance.heart--;
                GuestManager.instance.currentGuest.DecreseHeart();
                break;
            default:
                break;
        }
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
