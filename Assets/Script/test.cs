using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 5; j++)
			{
                Debug.LogFormat("i : {0}，j : {1}",i ,j);
				if (j==2)
				{
					
                    break;
                }
            }
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
