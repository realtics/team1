using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLoadSceneButton : LoadSceneButton
{
	public override void GotoScene()
	{
		Debug.Log(PlayerDataManager.Inst.GetPlayerData().fatigability);

		if (PlayerDataManager.Inst.GetPlayerData().fatigability == 0)
		{
			return;
		}
		else PlayerDataManager.Inst.GetPlayerData().fatigability--;

		Debug.Log(PlayerDataManager.Inst.GetPlayerData().fatigability);
		LoadingProgress.LoadScene((int)wantToGoSceneName);
	}
}
