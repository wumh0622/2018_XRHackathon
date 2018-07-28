using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using VRTK;

public class RayCastUIComponent : MonoBehaviour {

    [SerializeField] LayerMask LM;
    UIButtonRayTargetComponent temp;
    [SerializeField]VRTK_ControllerEvents controllerEvent;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(controllerEvent.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TouchpadPress))
		{
            RaycastHit hit;
            //Debug.DrawRay(transform.position, transform.forward * 100, Color.red, 100);
            if(Physics.Raycast(transform.position, transform.forward * 100 , out hit, 100, LM))
			{
				hit.collider.gameObject.GetComponent<UIButtonRayTargetComponent>().PointOn();
                temp = hit.collider.gameObject.GetComponent<UIButtonRayTargetComponent>();
                if(controllerEvent.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TriggerClick))
				{
                    hit.collider.gameObject.GetComponent<UIButtonRayTargetComponent>().ClickDown();
                }
				else
				{
                    hit.collider.gameObject.GetComponent<UIButtonRayTargetComponent>().ClickUp();
                }
            }
			else
			{
				if(temp != null)
                temp.PointExit();
            }
		}
        else
        {
			if(temp != null)
                temp.PointExit();
        }


    }
}
