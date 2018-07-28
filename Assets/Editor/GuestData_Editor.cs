using UnityEngine;
using UnityEditor;

//#if Editor
[CustomEditor(typeof(GuestData))]
public class GuestData_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        GuestData _guestData = (GuestData)target;
        serializedObject.Update();

        

        _guestData.IsRemove = EditorGUILayout.Toggle("IsReomve", _guestData.IsRemove);
        
        if (!_guestData.IsRemove)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_question"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_answers"), true);

            if (GUILayout.Button("新增"))
            {
                _guestData.AddSentence();
                
            }
        }
        else
        {
            _guestData._level = EditorGUILayout.IntField("Level", _guestData._level);
            if (GUILayout.Button("刪除"))
            {
                _guestData.RemoveLevel();
                _guestData.SortLevel();
            }
        }

        GUILayout.Space(10);
        /*if (GUILayout.Button("Sort Level"))
        {
            _guestData.SortLevel();
        } */

        GUILayout.Space(5);
        GUILayout.Label("下面是Datas", EditorStyles.boldLabel);
        GUILayout.Space(5);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Guest_Name"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("myActions"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("mytalks"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("myneeds"), true);
        //DrawDefaultInspector();
        //base.OnInspectorGUI();

        serializedObject.ApplyModifiedProperties();
    }

}
//#endif