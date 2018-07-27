using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRCharacterControllerEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResetAutoGrab(VRTK_ObjectAutoGrab autoGrab)
	{
        Debug.Log("RE");
        //autoGrab.ClearPreviousClone();
        //autoGrab.enabled = false;
    }
}
