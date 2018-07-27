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
        

        EditorGUILayout.PropertyField(serializedObject.FindProperty("Guest_Name"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("mytalks"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("myneeds"), true);
        //DrawDefaultInspector();
        //base.OnInspectorGUI();

        if (GUILayout.Button("Sort Level & ID"))
        {
            _guestData.SortID();
        }

        _guestData.IsRemove = EditorGUILayout.Toggle("IsReomve", _guestData.IsRemove);

        if (!_guestData.IsRemove)
        {
            _guestData._level = EditorGUILayout.IntField("Level", _guestData._level);
            _guestData._sec = EditorGUILayout.TextField("輸入新增字句", _guestData._sec);
            EditorGUILayout.LabelField("以新增字句的ID", _guestData._id.ToString());

            if (GUILayout.Button("新增"))
            {
                _guestData.AddSentence();
            }
        }
        else
        {
            _guestData._id = EditorGUILayout.TextField("輸入刪除字句的ID", _guestData._id);
            if (GUILayout.Button("刪除"))
            {
                _guestData.RemoveSentence();
                _guestData.SortID();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

}
//#endif