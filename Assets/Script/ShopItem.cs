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


    void Start()
    {
        //cardPrefab = CardManager.instance.cardObj;
    }
    public override void StartUsing(VRTK_InteractUse currentUsingObject)
	{
        base.StartUsing(currentUsingObject);
        Debug.Log("StartUsing");
        //currentUsingObject.gameObject.GetComponent<VRTK_InteractGrab>()
        //GameObject _obj = 
        GameObject clone = Instantiate(cardPrefab, currentUsingObject.gameObject.transform.localPosition, Quaternion.identity);
        CardManager.instance.GetCardData(cardToGet, clone.GetComponent<CardBase>());
        clone.transform.position = currentUsingObject.gameObject.transform.position;
        //currentUsingObject.gameObject.GetComponent<VRTK_ObjectAutoGrab>().ClearPreviousClone();
        currentUsingObject.gameObject.GetComponent<VRTK_InteractTouch>().ForceTouch(clone);
        currentUsingObject.gameObject.GetComponent<VRTK_InteractGrab>().AttemptGrab();
        currentUsingObject.gameObject.GetComponent<VRTK_ObjectAutoGrab>().enabled = false;
        currentUsingObject.gameObject.GetComponent<VRTK_ObjectAutoGrab>().objectToGrab = clone.GetComponent<VRTK_InteractableObject>();
        currentUsingObject.gameObject.GetComponent<VRTK_ObjectAutoGrab>().enabled = true;
        
        
    }

}
