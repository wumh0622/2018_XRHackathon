using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGrabObjectFix : MonoBehaviour {

    GameObject target;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            target.transform.position = transform.position;
        }
    }
}
