using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CardEvent : VRTK.VRTK_InteractableObject {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void StartUsing(VRTK_InteractUse currentUsingObject)
	{
        base.StartUsing(currentUsingObject);
        Debug.Log("StartUsing");
        //currentUsingObject.gameObject.GetComponent<VRTK_InteractGrab>()
        //GameObject _obj = 
        GetComponent<CardBase>().isUse = true;
    }
}
