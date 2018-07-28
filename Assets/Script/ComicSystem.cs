using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicSystem : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        ContentProcess("string input");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ContentProcess(string input)
    {
        string phrase = "The quick brown fox jumps over the lazy dog.";
        string[] words = phrase.Split('%');
		
    }
}
