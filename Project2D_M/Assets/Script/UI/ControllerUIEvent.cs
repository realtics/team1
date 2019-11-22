using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerUIEvent : MonoBehaviour
{
    [SerializeField] private PlayerUiInput m_playerUiInput;
    // Start is called before the first frame update
    private void Start()
    {
        JoyStickConnect();
        NormalAttackConnect();
        JumpConnect();
    }

    private void JoyStickConnect()
    {
        JoyStick joyStick = this.transform.GetComponentInChildren<JoyStick>();
        EventTrigger eventTrigger = joyStick.transform.GetComponentInChildren<EventTrigger>();
        joyStick.playerInput = m_playerUiInput;
        //드레그
        EventTrigger.Entry dragEvent = new EventTrigger.Entry();
        dragEvent.eventID = EventTriggerType.Drag;
        dragEvent.callback.AddListener((BaseEventData) => joyStick.Drag(BaseEventData));
        eventTrigger.triggers.Add(dragEvent);

        //드레그 엔드
        EventTrigger.Entry dragEndEvent = new EventTrigger.Entry();
        dragEndEvent.eventID = EventTriggerType.EndDrag;
        dragEndEvent.callback.AddListener((BaseEventData) => joyStick.DragEnd());
        eventTrigger.triggers.Add(dragEndEvent);

        dragEndEvent = new EventTrigger.Entry();
        dragEndEvent.eventID = EventTriggerType.EndDrag;
        dragEndEvent.callback.AddListener((BaseEventData) => joyStick.StopPlayerMove());
        eventTrigger.triggers.Add(dragEndEvent);

        //클릭다운
        EventTrigger.Entry clickDownEvent = new EventTrigger.Entry();
        clickDownEvent.eventID = EventTriggerType.PointerDown;
        clickDownEvent.callback.AddListener((BaseEventData) => joyStick.Click());
        eventTrigger.triggers.Add(clickDownEvent);

        //클릭업
        EventTrigger.Entry clickUpEvent = new EventTrigger.Entry();
        clickUpEvent.eventID = EventTriggerType.PointerUp;
        clickUpEvent.callback.AddListener((BaseEventData) => joyStick.StopPlayerMove());
        eventTrigger.triggers.Add(clickUpEvent);
    }

    private void NormalAttackConnect()
    {
        EventTrigger.Entry dounEvent = new EventTrigger.Entry();
        dounEvent.eventID = EventTriggerType.PointerDown;
        dounEvent.callback.AddListener(BaseEventData => m_playerUiInput.AttackInput());

        this.transform.Find("NormalAttackButton").GetComponent<EventTrigger>().triggers.Add(dounEvent);
    }

    private void JumpConnect()
    {
        EventTrigger.Entry dounEvent = new EventTrigger.Entry();
        dounEvent.eventID = EventTriggerType.PointerDown;
        dounEvent.callback.AddListener(BaseEventData => m_playerUiInput.JumpInput());

        this.transform.Find("JumpButton").GetComponent<EventTrigger>().triggers.Add(dounEvent);
    }
}
