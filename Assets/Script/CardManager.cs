using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    [System.Serializable]
    public class CardData
    {
        [Tooltip("對話卡才需要")]
        public GuestManager.GuestName guestN;
        [Tooltip("此卡種類")]
        public CardSpecies cardSpecies;
        [Tooltip("此卡數據鑰匙")]
        public CardName cardName;
        [Tooltip("此卡名稱")]
        public string cardNameText;
        [Tooltip("此卡圖片")]
        public Sprite cardImage;
        [Tooltip("購買金錢")]
        public int needMoney;
        [Tooltip("此卡數量")]
        public int cardAmount;
        [Tooltip("此卡描述")]
        public string description;
        [Tooltip("此卡述說的字句")]
        public string cardText;
    }

    public enum CardSpecies
    {
        AllUse,
        Dialogue,
        Commodity,
        Effect
    }

    public enum CardName
    {
        Null,

        A1,
        A2,
        A3,
        B1,
        B2,
        B3,
        C1,
        C2,
        C3,
        D1,
        D2,
        D3,
        Exit
    }

    public Transform[] diaCardSpawnPoint;
    public List<CardData> dialogueCardData = new List<CardData>();
    public List<CardData> myCardData = new List<CardData>();
    public Dictionary<CardName, CardData> DataBase = new Dictionary<CardName, CardData>();
    public List<CardData> ShopData = new List<CardData>();

    public GameObject cardObj;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        SetDataBase();
    }
    //設定數據
    void SetDataBase()
    {
        for (int i = 0; i < myCardData.Count; i++)
        {
            DataBase.Add(myCardData[i].cardName, myCardData[i]);
            if (myCardData[i].cardSpecies == CardSpecies.Commodity)
                ShopData.Add(myCardData[i]);
        }
    }

    //取得卡牌整個資料
    public void GetCardData(CardName _name, CardBase _base)
    {
        CardData data = null;
        DataBase.TryGetValue(_name, out data);
        if (data != null)
        {
            if (data.cardAmount > 0)
            {
                DataBase[_name].cardAmount--;
                //生成(位子旋轉自己改)
               // 
                _base.SetCardData(data);
            }
        }
    }

    //取得單個數據
    public CardData GetCorrectData(CardName _name)
    {
        CardData data = null;
        DataBase.TryGetValue(_name, out data);
        return data;
    }

    //購買後
    public void BuyOneNewCard(CardName _name)
    {
        if (DataBase.ContainsKey(_name))
            DataBase[_name].cardAmount++;        
    }

    //取得卡牌功能
    /*public Action GetCardFunction(CardName _name)
    {
        switch (_name)
        {
            case CardName.test1:
                return Test;
            default:
                return null;
        }
    }*/

    public int GetCardAmount(CardName _name)
    {
        return DataBase[_name].cardAmount;
    }

    #region 功能數據
    void Test()
    {
        Debug.Log("test成功");
    }
    #endregion

    public void InitialDiaCard(List<CardData> TalkCards)
    {
        //清除用
        /*foreach (var point in diaCardSpawnPoint)
        {
            if (point.gameObject.GetComponentInChildren<CardBase>() != null)
            {
                Destroy(point.gameObject.GetComponentInChildren<CardBase>().gameObject);
            }
        }*/
        Debug.Log("InitialDiaCard");
        for (int i = 0; i < TalkCards.Count; i++)
        {
            
            GameObject _card = Instantiate(cardObj, diaCardSpawnPoint[i]);
            _card.transform.position = diaCardSpawnPoint[i].transform.position;
            _card.GetComponent<CardBase>().SetCardData(TalkCards[i]);
            Debug.Log("_card's cardName" + _card.GetComponent<CardBase>().cardName);
        }
    }

    public void ClerAllSpace()
    {
        foreach (var point in diaCardSpawnPoint)
        {
            if (point.gameObject.GetComponentInChildren<CardBase>() != null)
            {
                Destroy(point.gameObject.GetComponentInChildren<CardBase>().gameObject);
            }
        }
    }

    #region Editor編輯用
        public GuestManager.GuestName _guestN;
        [Tooltip("此卡種類")]
        public CardSpecies _cardSpecies;
        [Tooltip("此卡數據鑰匙")]
        public CardName _cardName;
        [Tooltip("此卡名稱")]
        public string _cardNameText;
        [Tooltip("此卡圖片")]
        public Sprite _cardImage;
        [Tooltip("購買金錢")]
        public int _needMoney;
        [Tooltip("此卡數量")]
        public int _cardAmount;
        [Tooltip("此卡描述")]
        public string _description;
        [Tooltip("此卡述說的字句")]
        public string _cardText;
    public CardData _NewCardData;
    public void AddNewData()
    {
        if (_NewCardData.guestN != GuestManager.GuestName.None)
        {
            CardData _NewCardData = new CardData();
            _NewCardData.guestN = _guestN;
            _NewCardData.cardSpecies = _cardSpecies;
            _NewCardData.cardName = _cardName;
            _NewCardData.cardNameText = _cardNameText;
            _NewCardData.cardImage = _cardImage;
            _NewCardData.needMoney = _needMoney;
            _NewCardData.cardAmount = _cardAmount;
            _NewCardData.description = _description;
            _NewCardData.cardText = _cardText;
            dialogueCardData.Add(_NewCardData);
        }
        else
        {
            Debug.LogError("GuestName can't null");
        }
    }

    public int MinNumber, MaxNumber;
    public GuestManager.GuestName newGuestName;
    public void changeGuestName()
    {
        for (int i = MinNumber; i <= MaxNumber; i++)
        {
            dialogueCardData[i].guestN = newGuestName;
        }
    }
    #endregion
}
