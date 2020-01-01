using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpBuilder
{
	private Transform m_target = null;
	private string m_title = null;
	private string m_description = null;
	private List<PopupButtonInfo> m_buttonInfoList = null;

	public PopUpBuilder(Transform _target)
	{
		this.m_target = _target; this.m_buttonInfoList = new List<PopupButtonInfo>();
	}
	public void Build()
	{
		GameObject popupObject = GameObject.Instantiate(Resources.Load("Popup/" + "PopupPanel", typeof(GameObject))) as GameObject;
		popupObject.transform.SetParent(this.m_target, false);
		PopupPanel popupPanel = popupObject.GetComponent<PopupPanel>();

		popupPanel.SetTitle(this.m_title);
		popupPanel.SetDescription(this.m_description);
		popupPanel.SetButtons(this.m_buttonInfoList);
		popupPanel.Init();
	}
	public void SetTitle(string _title)
	{
		this.m_title = _title;
	}
	public void SetDescription(string _description)
	{
		this.m_description = _description;
	}
	public void SetButton(string _text, CallbackEvent _callback = null)
	{
		this.m_buttonInfoList.Add(new PopupButtonInfo(_text, _callback));
	}

}
