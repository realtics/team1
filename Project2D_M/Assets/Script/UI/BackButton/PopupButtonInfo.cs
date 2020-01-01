using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CallbackEvent();

public class PopupButtonInfo : MonoBehaviour
{
	// 버튼정보를 들고있는 클래스 - Builder에서 popup객체로 정보를 보낼때 사용 
	public string text = null;
	public CallbackEvent callback = null;
	public PopupButtonInfo(string _text, CallbackEvent _callback)
	{
		this.text = _text; 
		this.callback = _callback == null ? () => { } : _callback;
	}
}
