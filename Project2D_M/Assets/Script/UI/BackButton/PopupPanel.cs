using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupPanel : MonoBehaviour
{
	// 제목 text오브젝트 
	[SerializeField] private TextMeshProUGUI m_titleText = null;
	// 설명 text오브젝트 
	[SerializeField] private TextMeshProUGUI m_descriptionText = null;
	// 버튼생성시 버튼들의 부모, 레이아웃을 사용해 생성시마다 위치를 잡아준다. 
	[SerializeField] private GameObject m_buttonsLayout = null;
	// 버튼 프리팹 
	[SerializeField] private GameObject m_buttonPrefab = null;
	public void Init()
	{
		// 팝업등장 - 추가적인 초기화 정보는 여기에 구현, 팝업창생성시 확대되는 느낌같은 연출 등,
	}
	public void SetTitle(string _title)
	{
		this.m_titleText.text = _title;
	}
	public void SetDescription(string _description)
	{
		this.m_descriptionText.text = _description;
	}
	public void SetButtons(List<PopupButtonInfo> _popupButtonInfos)
	{
		foreach (var info in _popupButtonInfos)
		{ 
			GameObject buttonObject = Instantiate(this.m_buttonPrefab);
			buttonObject.transform.SetParent(this.m_buttonsLayout.transform, false);
			PopupButton popupButton = buttonObject.GetComponent<PopupButton>();
			popupButton.Init(info.text, info.callback, this.gameObject);
		}
	}

}
