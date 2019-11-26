using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
 * 작성자         : 박종현
 * 최종 수정 날짜 : 2019.11.22
 * 팀             : 1 Team
 * 스크립트 용도  : 이벤트 종류에 따라 요구되는 인스펙터를 다르게 설정하기 위해 만든 버튼 이벤트에디터 클래스. 
 *                  현재는 요구되는 종류가 게임오브젝트 밖에없어서 동일하지만 복잡해지면 차차 확장해나갈 예정.
*/

[CustomEditor(typeof(BaseButtonEvent))]
public class ButtonEventEditor : Editor
{
    enum EVENT_TYPE
    {
        OPEN,
        ANIMATION,
    }

    BaseButtonEvent m_buttonEvent;

    private void OnEnable()
    {
        m_buttonEvent = target as BaseButtonEvent;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Event Type");
        string[] eventNames = new string[] { "Open", "Menu" };
        int[] eventValues = new int[] { (int)EVENT_TYPE.OPEN, (int)EVENT_TYPE.ANIMATION };
        m_buttonEvent.eventNum = EditorGUILayout.IntPopup(m_buttonEvent.eventNum, eventNames,eventValues);
        EditorGUILayout.EndHorizontal();

        if (m_buttonEvent.eventNum == (int)EVENT_TYPE.OPEN)
        {
            EditorGUILayout.BeginHorizontal();
            m_buttonEvent.selectObject = (GameObject)EditorGUILayout.ObjectField("Select Object", m_buttonEvent.selectObject,typeof(GameObject),true);
            EditorGUILayout.EndHorizontal();
        }

        if (m_buttonEvent.eventNum == (int)EVENT_TYPE.ANIMATION)
        {
            EditorGUILayout.BeginHorizontal();
            m_buttonEvent.selectObject = (GameObject)EditorGUILayout.ObjectField("Select Object", m_buttonEvent.selectObject, typeof(GameObject), true);
            EditorGUILayout.EndHorizontal();
        }
        
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }

}
