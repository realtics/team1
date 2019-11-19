using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class JoyStick : MonoBehaviour
{
    private Transform m_stick;
    private Vector3 m_stickFirstPos;
    private Vector3 m_joyVec;
    private float m_radius;
    private Vector3 m_stickPos;

    void Start()
    {
        m_radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        m_stick = this.transform.Find("JoyStick");
        m_stickFirstPos = m_stick.transform.position;

        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        m_radius *= Can;
    }

    public void Drag(BaseEventData _Data)
    {
        PointerEventData data = _Data as PointerEventData;
        Vector3 pos = data.position;

        m_joyVec = (pos - m_stickFirstPos).normalized;

        float dis = Vector3.Distance(pos, m_stickFirstPos);

        if (dis < m_radius)
            m_stick.position = m_stickFirstPos + m_joyVec * dis;
        else
            m_stick.position = m_stickFirstPos + m_joyVec * m_radius;

        m_stickPos = m_joyVec;
    }

    // 드래그 끝.
    public void DragEnd()
    {
        m_stick.position = m_stickFirstPos;
        m_joyVec = Vector3.zero;

        m_stickPos = m_joyVec;
    }

    public int GetHorizontal()
    {
        if (m_stickPos.x < -0.5f)
            return -1;
        else if (m_stickPos.x > 0.5f)
            return 1;

        return 0;
    }
}
