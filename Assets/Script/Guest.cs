using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VRTK;

public class Guest : MonoBehaviour
{
    private NavMeshAgent nav;
    public GuestData mydata;
    public List<GuestManager.myAction> guestActions = new List<GuestManager.myAction>();
    public bool isWalk;
    int _actionInt = 0;
    bool isLeaving;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        nav.updateRotation = false;
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
                        Debug.Log("Question : " + mydata.mytalks[talkLevel].questiom.sentence);
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
        Debug.Log("下一個level : " + mydata.mytalks[talkLevel].answers.Find(x => x.NeedToTrigger == _card).OpenLevel);
        talkLevel = mydata.mytalks[talkLevel].answers.Find(x => x.NeedToTrigger == _card).OpenLevel;
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


