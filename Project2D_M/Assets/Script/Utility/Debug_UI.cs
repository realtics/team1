using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Debug_UI : Singletone<Debug_UI>
{
	public int Count = 0;
	private Dictionary<string,TextMeshProUGUI> m_arrText;

	//
	public void SetDebugText(string _str, string _text)
	{

		Count++;
	}

	public void AddTextObject(string _str, string _text)
	{
		//m_arrText[0].
		//TextMeshPro.gam
		//m_arrText.Add(_str, Instantiate<TextMeshPro>())
	}

	public void test()
	{
		SetDebugText("dfa", "dfa");
	}
}
