using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(CardManager))]
public class CardManager_Editor : Editor {

	public override void OnInspectorGUI()
	{
        CardManager _cardManager = (CardManager)target;

        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("dialogueCardData"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("myCardData"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("ShopData"), true);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardObj"));

        GUILayout.Space(10);
        GUILayout.Label("新增dialogueCardData用");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_guestN"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("_cardSpecies"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("_cardName"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("_cardNameText"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("_cardImage"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("_needMoney"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("_cardAmount"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("_description"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("_cardText"));
		GUILayout.Space(5);
        if (GUILayout.Button("新增"))
		{
            _cardManager.AddNewData();
        }

		GUILayout.Space(10);
		GUILayout.Label("改變GuestName用");
        _cardManager.MinNumber = EditorGUILayout.IntField("MinIndex", _cardManager.MinNumber);
		_cardManager.MaxNumber = EditorGUILayout.IntField("MaxIndex", _cardManager.MaxNumber);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("newGuestName"));
		GUILayout.Space(5);
        if(GUILayout.Button("改變GuestName"))
		{
            _cardManager.changeGuestName();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
