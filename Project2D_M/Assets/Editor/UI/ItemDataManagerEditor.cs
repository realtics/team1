using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemDataManager))]
public class ItemDataManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(((ItemDataManager)target).modifyValues)
        {
            if(GUILayout.Button("Save changes"))
            {
                ((ItemDataManager)target).DeserializeDictionary();
            }
        }
    }
}
