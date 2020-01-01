using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpControll : MonoBehaviour
{
	void Update()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if (Input.GetKey(KeyCode.Escape))
			{
				UIBackButton();
			}
		}
	}

	public void UIBackButton()
	{
		PopUpBuilder PopUpBuilder = new PopUpBuilder(this.transform);
		PopUpBuilder.SetTitle("게임종료");
		PopUpBuilder.SetDescription("게임을 종료하시겠습니까?");

		PopUpBuilder.SetButton("취소");
		PopUpBuilder.SetButton("확인", this.ExitGame);
		PopUpBuilder.Build();
	}

	private void ExitGame()
	{
		Application.Quit();
	}
}
