using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VRTK;
using UnityEditor;

public class Guest : MonoBehaviour
{
    private NavMeshAgent nav;
    private GuestData mydata;
    public GuestManager.GuestName guestName;
    public List<GuestManager.myAction> guestActions = new List<GuestManager.myAction>();
    public bool isWalk;
    int _actionInt = 0;
    bool isLeaving;

    private CardManager cardManager;
    private CardManager Card_Manager { get { if (cardManager == null) cardManager = CardManager.instance; return cardManager; } }

    void Awake()
    {
        string filePath = string.Format(@"Assets/{0}.asset", guestName);
        nav = GetComponent<NavMeshAgent>();
        mydata = (GuestData)AssetDatabase.LoadAssetAtPath(filePath, typeof(GuestData));
    }

    void Start()
    {
        nav.updateRotation = false;
        guestActions = mydata.myActions;
    }
    
    void Kill()
    {

                Destroy(gameObject);

    } 

    public void GuestMove(Vector3 _pos)
    {
        if (nav == null)
        {
            Debug.LogWarning("You Dont Have NavMesh");
            return;
        }
        nav.SetDestination(_pos);
        isWalk = true;
    }

    int talkLevel = 0;//用來選擇NPC對話組
    CardManager.CardName currentCardName;//NPC要求的產品
    int needCardNumber;//NPC要求產品數量
    void GuestAction(GuestManager.myAction _myaction)
    {
        switch (_myaction)
        {
            case GuestManager.myAction.Request:
                {
                    int ran = Random.Range(0, mydata.myneeds.Count);
                    currentCardName = mydata.myneeds[ran]._cardName;
                    needCardNumber = mydata.myneeds[ran].Number;
                    Debug.LogFormat("我要{0}，數量 : {1}", mydata.myneeds[ran]._cardName, mydata.myneeds[ran].Number);
                }
                break;
            case GuestManager.myAction.Talk:
                {
                    if (talkLevel < mydata.mytalks.Count)
                    {
                        //產生玩家對話卡
                        List<CardManager.CardData> tt = new List<CardManager.CardData>();
                        List<CardManager.CardData> playerTalkCards = Card_Manager.dialogueCardData.FindAll(x => x.guestN == guestName);
                        foreach (var card in playerTalkCards)
                        {
                            foreach (var cardOpen in mydata.mytalks[talkLevel].questiom.CardOpens)
                            {
                                if (card.cardName == cardOpen)
                                {
                                    tt.Add(card);
                                    break;
                                }
                            }
                        }
                        Debug.Log("Question : " + mydata.mytalks[talkLevel].questiom.sentence);
                        Card_Manager.InitialDiaCard(tt);
                    }
                    else
                    {
                        Debug.Log("超出範圍");
                    }
                }
                break;
            case GuestManager.myAction.Seller:
                Shop.instance.OpenShopMenu();
                break;
            default:
                break;
        }

        
    }

    public void GuestGoAction()
    {

        if (_actionInt > guestActions.Count-1)
        {
            GameFlow.instance.ToState(GameFlow.GameState.WaitingGuest);
            GuestLeaving();
        }
        else
        {
            GuestAction(guestActions[_actionInt]);
        }
    }

    //玩家回應客人Request，結束玩家動作時做
    public bool CompleteGuestNeed(CardManager.CardName _cardName, int _amount)
    {
        if (_cardName == currentCardName)
        {
            if (needCardNumber == _amount)
            {
                Debug.Log("完成");
                return true;
            }
            else
            {
                Debug.Log("數量不夠，失敗");
                return false;
            }
        }
        else
        {
            Debug.Log("失敗");
            return false;
        }      
    }
    //玩家回應NPC Question用的
    public void OnPlayerResponse(CardManager.CardName _card)
    {
        if (talkLevel >= mydata.mytalks.Count - 1)
        {
            talkLevel = 0;
            Debug.Log("以結尾，下面一位");
            return;
        }

        foreach (var item in mydata.mytalks[talkLevel].answers)
        {
            foreach (var i in item.NeedToTrigger)
            {
                if (i == _card)
                {
                    Debug.Log("下一個level : " + item.OpenLevel);
                    talkLevel = item.OpenLevel;
                    return;
                }
            }
        }
    }

    public bool FinishWalk()
    {
        if (isWalk == true)
        {
            if (nav.remainingDistance < nav.stoppingDistance)
            {
                return true;
            }
        }
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        CardBase cardGet = other.GetComponent<CardBase>();
        if(cardGet!=null)
        {
            if(guestActions[_actionInt] == GuestManager.myAction.Request)
            {
                CompleteGuestNeed(cardGet.cardName, 1);
                _actionInt++;
                GameFlow.instance.BackState();
            }
            else if(guestActions[_actionInt] == GuestManager.myAction.Talk)
            {
                OnPlayerResponse(cardGet.cardName);
                _actionInt++;
                GameFlow.instance.BackState();
            }
            Destroy(other.gameObject);
        }
    }

    void GuestLeaving()
    {
        nav.SetDestination(GameFlow.instance.guestLeaveTarget.position);
        Invoke("Kill", 3);
    }
}


