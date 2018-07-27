﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public GameObject cardObj;

    [System.Serializable]
    public class CardData
    {
        [Tooltip("此卡種類")]
        public CardSpecies cardSpecies;
        [Tooltip("此卡名稱")]
        public CardName cardName;
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
        test1,
        test2,
        test3,
        test4,
        test5
    }

    public List<CardData> myCardData = new List<CardData>();
    public Dictionary<CardName, CardData> DataBase = new Dictionary<CardName, CardData>();
    [HideInInspector]
    public List<CardData> ShopData = new List<CardData>();

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
            DataBase.Add(myCardData[0].cardName, myCardData[0]);
            if (myCardData[0].cardSpecies == CardSpecies.Commodity)
                ShopData.Add(myCardData[0]);
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
                //GameObject _obj = Instantiate(cardObj, transform.localPosition, Quaternion.identity);
                _base.SetCardData(data);
                //return _obj;
            }
        }
    }

    //購買後
    public void BuyOneNewCard(CardName _name)
    {
        if (DataBase.ContainsKey(_name))
            DataBase[_name].cardAmount++;        
    }

    //取得卡牌功能
    public Action GetCardFunction(CardName _name)
    {
        switch (_name)
        {
            case CardName.test1:
                return Test;
            default:
                return null;
        }
    }

    #region 功能數據
    void Test()
    {
        Debug.Log("test成功");
    }
    #endregion
}
