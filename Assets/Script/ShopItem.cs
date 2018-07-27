using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Highlighters;
[RequireComponent(typeof(VRTK_OutlineObjectCopyHighlighter))]
public class ShopItem : VRTK.VRTK_InteractableObject
{

    // Update is called once per frame

	public override void StartUsing(VRTK_InteractUse currentUsingObject)
	{
        base.StartUsing(currentUsingObject);
        Debug.Log("Use!!!");
	}

}
