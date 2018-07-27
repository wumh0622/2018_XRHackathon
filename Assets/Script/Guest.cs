using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guest : MonoBehaviour {
    private NavMeshAgent nav;
    [SerializeField] int TalkID;
    public GuestData mydata;
    public List<myAction> guestActions = new List<myAction>();
    //客人動作，買東西或談話
    public enum myAction
    {
        None,
		Request,
		Talk
    }
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    void Update()
	{
		if(Input.GetKeyDown("s"))
            GuestMove(Vector3.zero);
		if(Input.GetKeyDown("a"))
            GuestAction(myAction.Talk);
    }

    public void GuestMove(Vector3 _pos)
	{
		if(nav == null)
		{
            Debug.LogWarning("You Dont Have NavMesh");
            return;
        }
        nav.SetDestination(_pos);
    }

	public void GuestAction(myAction _action)
	{
		switch (_action)
		{
			case myAction.Request:
			{
                    Debug.Log("I need ");
            }break;
			case myAction.Talk:
			{
                    Debug.Log("I Just Talk ");
            }break;
            default:
                break;
        }
	}

	public void CompleteGuest(bool _Iscomplete)
	{
		if(_Iscomplete)
		{
            Debug.Log("完成");
        }
		else
		{
			Debug.Log("失敗");
		}
	}
}
