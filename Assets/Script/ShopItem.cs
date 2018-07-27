﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Highlighters;
[RequireComponent(typeof(VRTK_OutlineObjectCopyHighlighter))]
public class ShopItem : VRTK.VRTK_InteractableObject
{
    [Space(50)]
    [SerializeField]CardManager.CardName cardToGet;

    // Update is called once per frame

    public override void StartUsing(VRTK_InteractUse currentUsingObject)
	{
        base.StartUsing(currentUsingObject);
        GameObject clone = CardManager.instance.GetCardData(cardToGet);
        Debug.Log(currentUsingObject.name);
        currentUsingObject.gameObject.GetComponent<VRTK_ObjectAutoGrab>().objectToGrab = clone.GetComponent<VRTK_InteractableObject>();
    }

}
