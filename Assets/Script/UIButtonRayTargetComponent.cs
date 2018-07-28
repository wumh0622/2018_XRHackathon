using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(BoxCollider))]
public class UIButtonRayTargetComponent : MonoBehaviour {

	[Tooltip("手動更改觸發器大小 預設關閉")]
    [SerializeField] bool manualCollider = false;
    Button targetButton;
    BoxCollider boxCollider;
	PointerEventData pointer = new PointerEventData(EventSystem.current);
	bool isClick;
	

    void Awake()
	{
        if (!manualCollider)
        {
            targetButton = GetComponent<Button>();
            boxCollider = GetComponent<BoxCollider>();
            Canvas targetCanvas = GetComponentInParent<Canvas>();
            RectTransform targetRectTransform = targetCanvas.gameObject.GetComponent<RectTransform>();
            RectTransform targetButtonRectTransform = targetButton.gameObject.GetComponent<RectTransform>();
            boxCollider.size = new Vector3(targetButtonRectTransform.rect.width, targetButtonRectTransform.rect.height, 5);
        }
    }

	// Use this for initialization
	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PointOn()
	{
        print("PointOn");
        ExecuteEvents.Execute(targetButton.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
		pointer = new PointerEventData(EventSystem.current);
    }

	public void PointExit()
	{
        ExecuteEvents.Execute(targetButton.gameObject, pointer, ExecuteEvents.pointerExitHandler);
		pointer = new PointerEventData(EventSystem.current);
    }

	public void ClickDown()
	{
		if(isClick == false)
		{
			ExecuteEvents.Execute(targetButton.gameObject, pointer, ExecuteEvents.pointerDownHandler);
			ExecuteEvents.Execute(targetButton.gameObject, pointer, ExecuteEvents.pointerClickHandler);
			pointer = new PointerEventData(EventSystem.current);
			isClick = true;
		}
	}

	public void ClickUp()
	{
		if(isClick == true)
		{
			ExecuteEvents.Execute(targetButton.gameObject, pointer, ExecuteEvents.pointerUpHandler);
			//ExecuteEvents.Execute(targetButton.gameObject, pointer, ExecuteEvents.);
			pointer = new PointerEventData(EventSystem.current);
			isClick = false;
		}
	}

	public void clickEvent()
	{
        print("CLICK");
    }
}
