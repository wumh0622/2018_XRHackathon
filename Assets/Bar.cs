using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {

    [SerializeField] Image moneyI;
    [SerializeField] Image heartI;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        moneyI.fillAmount = (float)GameFlow.instance.money / (float)10;
		heartI.fillAmount = (float)GameFlow.instance.heart / (float)10;

    }
}
