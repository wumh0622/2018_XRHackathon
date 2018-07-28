﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{

    public static GameFlow instance;

    [SerializeField] int money = 10;
    [SerializeField] int heart = 10;

    [SerializeField] int WaitingTime;


    int guestNum = 0;
    float timer;
    float timeTemp = 0;

    [SerializeField] Transform guestWalkTarget;
    [SerializeField] public Transform guestLeaveTarget;


    public enum GameState
    {
        GameMenu,Initial, WaitingGuest, GuestSpawn, GuestComing, GuestTime, PlayerTime, GameOver
    }

    GameState currentState = GameState.GameMenu;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        Initial();
    }
    void Initial()
    {
        //初始化
        money = 10;
        heart = 10;
        guestNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameState.Initial:
                Initial();
                //CardManager.instance.InitialDiaCard();
                currentState = GameState.WaitingGuest;
                break;
            case GameState.WaitingGuest:
                Debug.Log("WaitingGuestSTART");
                timer += Time.deltaTime;
                if (timer > timeTemp + WaitingTime)
                {
                    Debug.Log("WaitingGuest");
                    timeTemp = timer;
                    currentState = GameState.GuestSpawn;
                }
                break;

            case GameState.GuestSpawn:
                if (!GuestManager.instance.InstantiateGuest())
                {
                    currentState = GameState.GameOver;
                }
                else
                {
                    currentState = GameState.GuestComing;
                }
                break;

            case GameState.GuestComing:
                if (GuestManager.instance.currentGuest.isWalk == false)
                {
                    Debug.Log("GuestComing");
                    GuestManager.instance.currentGuest.GuestMove(guestWalkTarget.position);
                }
                else
                {
                    if (GuestManager.instance.currentGuest.FinishWalk() == true)
                    {
                        Debug.Log("GuestManager.instance.currentGuest.FinishWalk() == true");
                        currentState = GameState.GuestTime;
                    }
                }
                break;
            case GameState.GuestTime:
                Debug.Log("GuestTime");
                currentState = GameState.PlayerTime;
                GuestManager.instance.currentGuest.GuestGoAction();
                break;
            case GameState.PlayerTime:

                break;

            case GameState.GameOver:
                Debug.Log("Over");
                break;
        }
    }


    public void NextState()
    {
        currentState = (GameState)((int)currentState + 1);
    }

    public void BackState()
    {
        currentState = (GameState)((int)currentState - 1);
    }

    public void ToState(GameState state)
    {
        currentState = state;
        print(state);
    }

    public bool DetuctMoney(int _needMoney)
    {
        if (_needMoney < money)
            return false;
        else
        {
            money -= _needMoney;
            return true;
        }

    }

    public void StartGmae(GameObject Canvas)
    {
        currentState = GameState.Initial;
        Canvas.SetActive(false);
    }
}
