using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guest : MonoBehaviour {
    //[SerializeField] List<>

    public NavMeshAgent nav;
    public enum myAction
    {
        None,
		Request,
		Talk
    }
	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
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
            Debug.LogWarning("NoNavMesh");
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

		}
		else
		{

		}
	}
}
