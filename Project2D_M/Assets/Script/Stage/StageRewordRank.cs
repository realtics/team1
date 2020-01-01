using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageRewordRank : MonoBehaviour
{
	public enum RANK_ENUM
	{
		SSS_RANK,
		SS_RANK,
		S_RANK,
		A_RANK,
		B_RANK,
		C_RANK,
		D_RANK,
		F_RANK,
	}

	[SerializeField] private Sprite[] rankSprites = null;
	[SerializeField] private Image rankImage = null;
	private RANK_ENUM currentEnum = RANK_ENUM.F_RANK;
	public void SetRankSprite(RANK_ENUM _rnakEnum)
	{
		rankImage.sprite = rankSprites[(int)_rnakEnum];
        rankImage.SetNativeSize();
        currentEnum = _rnakEnum;
	}

	public void PlusRankSprite()
	{
		if (currentEnum == 0)
			return;

		currentEnum--;
		rankImage.sprite = rankSprites[(int)currentEnum];
        rankImage.SetNativeSize();
    }
}
