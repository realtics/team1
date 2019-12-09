using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_19
 * 팀              : 1팀
 * 스크립트 용도   : 플에이어에 연결해 행동을 조절할 UI 조이스틱 관련 함수
 */
public class JoyStick : MonoBehaviour
{
    public PlayerInput playerInput { get; set; }
    private Transform m_stick;
    private Vector3 m_stickFirstPos;
    private Vector3 m_joyVec;
    private float m_radius;
    private Vector3 m_stickPos;

	private void Awake()
	{
		m_radius = GetComponent<RectTransform>().sizeDelta.y;// * 0.5f;
		m_stick = this.transform.Find("JoyStickCon");
		m_stickFirstPos = m_stick.transform.position;
		InitJoystick();

		float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
		m_radius *= Can;
	}

	public void InitJoystick()
	{
		m_stickFirstPos = this.transform.position;
		m_stick.position = m_stickFirstPos;
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
		m_stickFirstPos = this.transform.position;
		m_stick.position = m_stickFirstPos;

		m_joyVec = Vector3.zero;

        m_stickPos = m_joyVec;

        playerInput.JoyStickMove(PlayerInput.JOYSTICK_STATE.JOYSTICK_CENTER);
    }

    public void Click()
	{
		InitJoystick();
		StartCoroutine(nameof(PlayerMove));
    }

    public void StopPlayerMove()
    {
        StopCoroutine(nameof(PlayerMove));
        playerInput.JoyStickMove(PlayerInput.JOYSTICK_STATE.JOYSTICK_CENTER);
    }

    IEnumerator PlayerMove()
    {
        while(true)
        {
            if (m_stickPos.x < -0.5f)
                playerInput.JoyStickMove(PlayerInput.JOYSTICK_STATE.JOYSTICK_LEFT);
            else if (m_stickPos.x > 0.5f)
                playerInput.JoyStickMove(PlayerInput.JOYSTICK_STATE.JOYSTICK_RIGHT);
            else if (m_stickPos.y > 0.5f)
                playerInput.JoyStickMove(PlayerInput.JOYSTICK_STATE.JOYSTICK_UP);
            else if (m_stickPos.y < -0.5f)
                playerInput.JoyStickMove(PlayerInput.JOYSTICK_STATE.JOYSTICK_DOWN);
            else playerInput.JoyStickMove(PlayerInput.JOYSTICK_STATE.JOYSTICK_CENTER);

            yield return null;
        }
    }
}
