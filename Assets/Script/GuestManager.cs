using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    public static GuestManager instance;
    [SerializeField] GameObject[] GuestPerfabs;

    public enum GuestName
    {
        None,
        bigMom
    }
	//客人動作，買東西或談話
    public enum myAction
    {
        None,
		Request,
		Talk
    }
	
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void InstantiateGuest(int _Perfabsindex, Vector3 _pos, Quaternion _rot)
    {
        Instantiate(GuestPerfabs[_Perfabsindex], _pos, _rot);
    }
}
