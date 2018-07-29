using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VRTK;
using UnityEditor;

public class Guest : MonoBehaviour
{
    private NavMeshAgent nav;
    public GuestData mydata;
    public GuestManager.GuestName guestName;
    public List<GuestManager.myAction> guestActions = new List<GuestManager.myAction>();
    public bool isWalk;
    int _actionInt = 0;
    bool isLeaving;

    private CardManager cardManager;
    private CardManager Card_Manager { get { if (cardManager == null) cardManager = CardManager.instance; return cardManager; } }

    public Canvas delCanvas;

    void Awake()
    {
        string filePath = string.Format(@"Assets/{0}.asset", guestName);
        nav = GetComponent<NavMeshAgent>();
        //mydata = (GuestData)AssetDatabase.LoadAssetAtPath(filePath, typeof(GuestData));
    }

    void Start()
    {
        delCanvas = GetComponentInChildren<Canvas>();
        //nav.updateRotation = false;
        guestActions = mydata.myActions;
    }
    
    void Kill()
    {

        Destroy(gameObject);

    } 

    void Update()
    {
        if(nav.velocity.magnitude > .1f)
        {
            gameObject.GetComponent<Animator>().SetBool("isWalk", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("isWalk", false);
        }
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
    public List<CardManager.CardData> tt;
    void GuestAction(GuestManager.myAction _myaction)
    {
        switch (_myaction)
        {
            case GuestManager.myAction.Request:
                {
                    int ran = Random.Range(0, mydata.myneeds.Count);
                    currentCardName = mydata.myneeds[ran]._cardName;
                    needCardNumber = mydata.myneeds[ran].Number;
                    GetComponent<Animator>().SetInteger("Random", Random.Range(0, 2));
                    GetComponent<Animator>().SetTrigger("Speek");
                    ComicSystem.instance.ContentProcess( string.Format("我要{0}，數量 : {1}", mydata.myneeds[ran]._cardName, mydata.myneeds[ran].Number), GameFlow.GameState.PlayerTime);
                }
                break;
            case GuestManager.myAction.Talk:
                {
                    if (talkLevel < mydata.mytalks.Count)
                    {
                        //產生玩家對話卡
                       /* List<CardManager.CardData>*/tt = new List<CardManager.CardData>();
                        List<CardManager.CardData> playerTalkCards = Card_Manager.dialogueCardData.FindAll(x => x.guestN == guestName);
                        foreach (var card in playerTalkCards)
                        {
                            foreach (var cardOpen in mydata.mytalks[talkLevel].questiom.CardOpens)
                            {
                                if (card.cardName == cardOpen)
                                {
                                    tt.Add(card);
                                    for (int i = 0; i < tt.Count; i++)
                                    {

                                        Debug.Log("tt的cardName" + tt[i].cardName);
                                    }
                                    break;
                                }
                            }
                        }
                                            GetComponent<Animator>().SetInteger("Random", Random.Range(0, 2));
                    GetComponent<Animator>().SetTrigger("Speek");
                        ComicSystem.instance.ContentProcess(string.Format("Question : " + mydata.mytalks[talkLevel].questiom.sentence),GameFlow.GameState.PlayerTime);
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
        Card_Manager.ClerAllSpace();

        if (talkLevel >= mydata.mytalks.Count - 1)
        {
            talkLevel = 0;
            Debug.Log("以結尾，下面一位");
            _actionInt = 9999;
            GameFlow.instance.ToState(GameFlow.GameState.GuestTime);
            return;
        }

        foreach (var item in mydata.mytalks[talkLevel].answers)
        {
            foreach (var i in item.NeedToTrigger)
            {
                if (i == _card)
                {
                    if (item.OpenLevel < 0)
                    {
                        talkLevel = 0;
                        Debug.Log("以結尾，下面一位");
                        _actionInt = 9999;
                        GameFlow.instance.ToState(GameFlow.GameState.GuestTime);
                        return;
                    }
                    ComicSystem.instance.ContentProcess(item.sentence,GameFlow.GameState.GuestTime);
                    Debug.Log("下一個level : " + item.OpenLevel);
                    talkLevel = item.OpenLevel;
                    return;
                }
                else
                {
                    
                    Debug.Log("ERRORRRRR" + _card);
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
        if(GameFlow.instance.currentState == GameFlow.GameState.PlayerTime)
        {
        CardBase cardGet = other.GetComponent<CardBase>();
            if (cardGet != null)
            {
                if (guestActions[_actionInt] == GuestManager.myAction.Request)
                {
                    CompleteGuestNeed(cardGet.cardName, 1);
                    _actionInt++;
                    cardGet.StartFunction();
                    //GameFlow.instance.BackState();
                }
                else if (guestActions[_actionInt] == GuestManager.myAction.Talk)
                {
                    OnPlayerResponse(cardGet.cardName);
                    _actionInt++;
                    cardGet.StartFunction();
                    //GameFlow.instance.BackState();
                }
                Destroy(other.gameObject);
            }
        }
    }

    void GuestLeaving()
    {
        nav.SetDestination(GameFlow.instance.guestLeaveTarget.position);
        Invoke("Kill", 3);
    }

    public void openDel()
    {
        Debug.Log("openDel");
        delCanvas.GetComponent<Animator>().SetBool("Open", true);
        delCanvas.GetComponent<Animator>().SetBool("Close", false);
    }

    public void CloseDel()
    {
        Debug.Log("CloseDel");
        delCanvas.GetComponent<Animator>().SetBool("Open",false);
        delCanvas.GetComponent<Animator>().SetBool("Close", true);
    }

    public void Reset()
    {
        delCanvas.GetComponent<Animator>().SetBool("Open",false);
        delCanvas.GetComponent<Animator>().SetBool("Close", false);
    }
}


