using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    private CardManager cardManager;
    private CardManager Card_Manager { get { if (cardManager == null) cardManager = CardManager.instance; return cardManager; } }
    [Tooltip("卡片欲置物")]
    public GameObject cardObj;
    [Tooltip("第一張卡位子")]
    [SerializeField] Transform firstPos;
    [Tooltip("發牌位子")]
    [SerializeField] Transform lincesPos;
    [Tooltip("每張卡間隔")]
    [SerializeField] float cardGap;
    private int nowIndex = 0;

    [Tooltip("一次商店總共賣多少卡")]
    [SerializeField] int allShopCard;

    private List<GameObject> nowShopList = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

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
    }

    public void OpenShopMenu()
    {
        RandomNextOne();
    }
    public void CloseShopMenu()
    {
        ClearAll();
    }

    private void RandomNextOne()
    {
        if (nowIndex == allShopCard)
        {
            GameObject obj = Instantiate(cardObj, lincesPos.localPosition, Quaternion.identity);

            obj.GetComponent<CardBase>().SetCardData(CardManager.instance.GetCorrectData(CardManager.CardName.Exit));
            obj.transform.DOMove(firstPos.localPosition + new Vector3(nowIndex * cardGap, 0, 0), 0.65f).SetEase(Ease.OutQuart);
            nowShopList.Add(obj);
            nowIndex = 0;
            return;
        }
        else
        {
            int r = Random.Range(0, Card_Manager.ShopData.Count);
            StartCoroutine(RandomCard(r));
        }
    }

    IEnumerator RandomCard(int _r)
    {
        GameObject obj = Instantiate(cardObj, lincesPos.localPosition, Quaternion.identity);
        obj.GetComponent<CardBase>().SetCardData(Card_Manager.ShopData[_r]);
        nowShopList.Add(obj);
        if (nowIndex == 0)
        {
            nowIndex++;
            obj.transform.DOMove(firstPos.position, 0.65f).SetEase(Ease.OutQuart); ;
            yield return new WaitForSeconds(0.5f);
            RandomNextOne();
        }
        else
        {
            obj.transform.DOMove(firstPos.localPosition + new Vector3(nowIndex * cardGap, 0, 0), 0.65f).SetEase(Ease.OutQuart);
            nowIndex++;
            yield return new WaitForSeconds(0.5f);
            RandomNextOne();
        }
    }

    void ClearAll()
    {
        for (int i = 0; i < nowShopList.Count; i++)
        {
            if (nowShopList[i] != null)
                Destroy(nowShopList[i]);
        }
    }

}
