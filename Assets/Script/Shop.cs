using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    private CardManager cardManager;
    private CardManager Card_Manager { get { if (cardManager == null) cardManager = CardManager.instance; return cardManager; } }
    [Tooltip("卡片欲置物")]
    public GameObject cardObj;
    [Tooltip("第一張卡位子")]
    [SerializeField] Transform firstPos;
    [Tooltip("一次商店總共賣多少卡")]
    [SerializeField] int allShopCard;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    /*
    [SerializeField] CardBase test;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            OpenShopMenu();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            test.BuyThisCard();
        }
    }*/

    public void OpenShopMenu()
    {
        for (int i = 0; i < allShopCard; i++)
        {
            int r = Random.Range(0, Card_Manager.ShopData.Count);
            RandomCard(r, firstPos.localPosition + new Vector3(i, 0, 0));
        }
    }

    void RandomCard(int _r, Vector3 _pos)
    {
        GameObject obj = Instantiate(cardObj, _pos, Quaternion.identity);
        obj.GetComponent<CardBase>().SetCardData(Card_Manager.ShopData[_r]);
    }
}
