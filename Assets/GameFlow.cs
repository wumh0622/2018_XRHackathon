using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{

    public static GameFlow instance;

    [SerializeField] int money = 10;
    [SerializeField] int heart = 10;

    [SerializeField] int WaitingTime;

    [SerializeField] GameObject[] guestArray;
    int guestNum = 1;


    public enum GameState
    {
        Initial, WaitingGuest, GuestComing, GuestTime, PlayerTime, GameOver
    }

    GameState currentState = GameState.Initial;

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
        guestNum = 1;
	}

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
			case GameState.Initial:
				Initial();
				currentState = GameState.WaitingGuest;
                break;
            case GameState.WaitingGuest:
                if (Time.timeSinceLevelLoad > WaitingTime)
                {
                    Debug.Log("GuestComing");
                    currentState = GameState.GuestComing;
                }
                break;

			case GameState.GuestComing:
            default:

                break;
        }
    }
}
