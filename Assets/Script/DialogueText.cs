using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueText : MonoBehaviour
{
    [SerializeField] Text dialogueText;
    
    public void SetText(string _text)
    {
        dialogueText.text = _text;
    }

}
