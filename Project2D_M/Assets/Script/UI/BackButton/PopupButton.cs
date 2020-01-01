using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupButton : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI buttonString = null;
	private GameObject m_target = null;
	private CallbackEvent m_callbackEvent = null;
	public void Init(string _text, CallbackEvent _callback, GameObject _target)
	{
		// 초기화 - 매개변수로 받은 이름과 콜백함수로 클릭시 콜백함수를 호출해주는 팝업버튼 
		this.buttonString.text = _text;
		this.m_callbackEvent = _callback;
		this.m_target = _target;
	}
	public void OnButton()
	{
		this.m_callbackEvent();
		Destroy(m_target);
	}
}
