using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Highlighters;
[RequireComponent(typeof(VRTK_OutlineObjectCopyHighlighter))]
public class ShopItem : VRTK.VRTK_InteractableObject
{
    [Space(50)]
    [SerializeField]CardManager.CardName cardToGet;
    GameObject cardPrefab;

    GameObject clone;

    VRTK_InteractUse controller;


    void Start()
    {
        cardPrefab = CardManager.instance.cardObj;
    }

    void Update()
    {
        if (controller != null && clone != null)
        {
            //currentUsingObject.gameObject.GetComponent<VRTK_ObjectAutoGrab>().ClearPreviousClone();
            Debug.Log(controller.gameObject.name);
            controller.gameObject.GetComponent<VRTK_InteractTouch>().ForceTouch(clone);
            /* currentUsingObject.gameObject.GetComponent<VRTK_InteractGrab>().AttemptGrab(); */
            controller.gameObject.GetComponent<VRTK_ObjectAutoGrab>().enabled = false;
            controller.gameObject.GetComponent<VRTK_ObjectAutoGrab>().objectToGrab = clone.GetComponent<VRTK_InteractableObject>();
            controller.gameObject.GetComponent<VRTK_ObjectAutoGrab>().enabled = true;
            if(controller.gameObject.GetComponent<VRTK_InteractGrab>().GetGrabbedObject() != null)
            {
                controller = null;
                clone = null;
            }
        }
    }

    public override void StartUsing(VRTK_InteractUse currentUsingObject)
	{
        base.StartUsing(currentUsingObject);
        Debug.Log("StartUsing");
        //currentUsingObject.gameObject.GetComponent<VRTK_InteractGrab>()
        //GameObject _obj = 
        if (CardManager.instance.GetCardAmount(cardToGet) > 0)
        {
            clone = Instantiate(cardPrefab, currentUsingObject.gameObject.transform.localPosition, Quaternion.identity);
            CardManager.instance.GetCardData(cardToGet, clone.GetComponent<CardBase>());
            clone.transform.position = currentUsingObject.gameObject.transform.position;
            controller = currentUsingObject;
        }
    }

}
