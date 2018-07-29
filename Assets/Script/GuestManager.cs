using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    public static GuestManager instance;
    [SerializeField] GameObject[] GuestPerfabs;

    [SerializeField] Transform GuestStartPoint;
    public Guest currentGuest;

    public int guestIndex;

    public enum GuestName
    {
        None,
        bigMom,
        oldPostman,
        girl
    }
	//客人動作，買東西或談話
    public enum myAction
    {
        None,
		Request,
		Talk,
        Seller
    }
	
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public bool InstantiateGuest()
    {
        if (guestIndex <= GuestPerfabs.Length - 1)
        {
            GameObject clone = Instantiate(GuestPerfabs[guestIndex], GuestStartPoint.position, Quaternion.identity);
            guestIndex++;
            currentGuest = clone.GetComponent<Guest>();
            return true;
        }
        else
        {
            return false;
        }

    }
}
