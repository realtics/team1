using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_25
 * 팀              : 1팀
 * 스크립트 용도   : UI컨트롤러와 PlayerUiInput스크립트 연결 스크립트
 */
public class ControllerUIEvent : MonoBehaviour
{
	[SerializeField] private GameObject m_Player = null;
	private PlayerInput m_playerInput = null;
    [SerializeField] private JoyStick m_joyStick = null;
    [SerializeField] private EventTrigger m_normalAttackEvent = null;
    [SerializeField] private EventTrigger m_jumpEvent = null;
	[SerializeField] private EvasionButton m_evasionButton = null;
	[SerializeField] private SkillQuick skillQuick1 = null;
	[SerializeField] private SkillQuick skillQuick2 = null;
	[SerializeField] private SkillQuick skillQuick3 = null;
	[SerializeField] private SkillQuick skillQuick4 = null;

	private PlayerEvasion m_playerEvasion = null;

	// Start is called before the first frame update
	private void Start()
    {
		if(m_Player == null)
			m_playerInput = GameObject.Find("Player").GetComponent<PlayerInput>(); 
		else m_playerInput = m_Player.GetComponent<PlayerInput>();

		m_playerEvasion = m_Player.GetComponent<PlayerEvasion>();

		JoyStickConnect();
        NormalAttackConnect();
        JumpConnect();
        EvasionConnect();

		skillQuick1.InitQuickSkill("FlameHaze", m_playerInput);
		skillQuick2.InitQuickSkill("FireBallShoot", m_playerInput);
		skillQuick3.InitQuickSkill("FireBoomShoot", m_playerInput);
		skillQuick4.InitQuickSkill(null, m_playerInput);
	}

    private void JoyStickConnect()
    {
        EventTrigger eventTrigger = m_joyStick.transform.GetComponentInChildren<EventTrigger>();
        m_joyStick.playerInput = m_playerInput;
        //드레그
        EventTrigger.Entry dragEvent = new EventTrigger.Entry();
        dragEvent.eventID = EventTriggerType.Drag;
        dragEvent.callback.AddListener((BaseEventData) => m_joyStick.Drag(BaseEventData));
        eventTrigger.triggers.Add(dragEvent);

        //드레그 엔드
        EventTrigger.Entry dragEndEvent = new EventTrigger.Entry();
        dragEndEvent.eventID = EventTriggerType.EndDrag;
        dragEndEvent.callback.AddListener((BaseEventData) => m_joyStick.DragEnd());
        eventTrigger.triggers.Add(dragEndEvent);

        dragEndEvent = new EventTrigger.Entry();
        dragEndEvent.eventID = EventTriggerType.EndDrag;
        dragEndEvent.callback.AddListener((BaseEventData) => m_joyStick.StopPlayerMove());
        eventTrigger.triggers.Add(dragEndEvent);

        //클릭다운
        EventTrigger.Entry clickDownEvent = new EventTrigger.Entry();
        clickDownEvent.eventID = EventTriggerType.PointerDown;
        clickDownEvent.callback.AddListener((BaseEventData) => m_joyStick.Click());
        eventTrigger.triggers.Add(clickDownEvent);

        //클릭업
        EventTrigger.Entry clickUpEvent = new EventTrigger.Entry();
        clickUpEvent.eventID = EventTriggerType.PointerUp;
        clickUpEvent.callback.AddListener((BaseEventData) => m_joyStick.StopPlayerMove());
        eventTrigger.triggers.Add(clickUpEvent);
    }

    private void NormalAttackConnect()
    {
        EventTrigger.Entry dounEvent = new EventTrigger.Entry();
        dounEvent.eventID = EventTriggerType.PointerDown;
        dounEvent.callback.AddListener(BaseEventData => m_playerInput.AttackInput());

        m_normalAttackEvent.triggers.Add(dounEvent);
    }

    private void JumpConnect()
    {
		EventTrigger.Entry dounEvent = new EventTrigger.Entry();
        dounEvent.eventID = EventTriggerType.PointerDown;
        dounEvent.callback.AddListener(BaseEventData => m_playerInput.JumpInput());

		m_jumpEvent.triggers.Add(dounEvent);
    }

    private void EvasionConnect() 
    {
		m_playerEvasion.evasionButton = m_evasionButton;
		EventTrigger evasionEvent = m_evasionButton.GetComponent<EventTrigger>();

		EventTrigger.Entry dounEvent = new EventTrigger.Entry();
        dounEvent.eventID = EventTriggerType.PointerDown;
        dounEvent.callback.AddListener(BaseEventData => m_playerInput.EvasionInput());

		evasionEvent.triggers.Add(dounEvent);
    }
}
