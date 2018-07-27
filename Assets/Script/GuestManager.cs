using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour {

	public static GuestManager instance;

	public enum GuestName
	{
		None,
		Allen
	}

	void Awake()
	{
		if (instance == null)
            instance = this;
        else
            Destroy(this);
	}

	public void InstantiateGuest(){
		
	}
}
