using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    private CardManager cardManager;
    private CardManager Card_Manager { get { if (cardManager == null) cardManager = CardManager.instance; return cardManager; } }

    [SerializeField] int allShopCard;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void OpenShopMenu()
    {
        for (int i = 0; i < allShopCard; i++)
        {
            int r = Random.Range(0, Card_Manager.ShopData.Count);
            RandomCard(r);
        }
    }

    void RandomCard(int _r)
    {
      //  Card_Manager.ShopData[_r].
    }
    

}
