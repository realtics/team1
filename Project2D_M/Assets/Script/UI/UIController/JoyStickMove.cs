using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class JoyStickMove : MonoBehaviour
{
	public GameObject Joystick;
	private JoyStick m_joyStick;
	private void Start()
	{
		m_joyStick = Joystick.GetComponent<JoyStick>();
		m_joyStick.InitJoystick();
		Joystick.SetActive(false);

	}
	public void OnJoystick(BaseEventData _data)
	{
		Joystick.SetActive(true);
		PointerEventData data = _data as PointerEventData;
		Joystick.transform.position = data.position;
		m_joyStick.Click();
	}

	public void OffJoystick(BaseEventData _data)
	{	
		m_joyStick.StopPlayerMove();
		m_joyStick.DragEnd();
		Joystick.SetActive(false);
	}

	public void DragJoystick(BaseEventData _data)
	{
		m_joyStick.Drag(_data);
	}
}
