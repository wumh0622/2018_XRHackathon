using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComicSystem : MonoBehaviour
{
    public static ComicSystem instance;
    int index;
    Guest guest;
    GameFlow.GameState nextState;
    string[] words;

	void Awake()
	{
		if(instance== null)
		{
            instance = this;
        }
	}

    // Use this for initialization

    public void ContentProcess(string input, GameFlow.GameState next)
    {
        index = 0;
        guest = GuestManager.instance.currentGuest;
        guest.Reset();
        nextState = next;
        words = input.Split('%');
        DisplayContent();
    }

	void DisplayContent()
	{
        if (index < words.Length)
        {
            Debug.Log("DisplayContent");
            guest.gameObject.GetComponentInChildren<Text>().text = words[index];
            guest.openDel();
        }
		else
		{
            GameFlow.instance.ToState(nextState);
        }
    }

	public void nextContent()
	{
	 	guest.CloseDel();
        index++;
        DisplayContent();
    }
	
}
