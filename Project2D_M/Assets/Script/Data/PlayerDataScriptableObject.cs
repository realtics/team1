using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_25
 * 팀              : 1팀
 * 스크립트 용도   : 플레이어 데이터를 ScriptableObject로 게임 테스트를 편리하게 하기위한 스크립트
 */

[CreateAssetMenu(fileName = "PlayerDataScript", menuName = "PlayerData", order = 1)]
[System.Serializable]
public class PlayerDataScriptableObject : ScriptableObject
{
	public string charactorName;
	public int level;
	public int exp;
	public int gold;
	public int cash;
	public int fatigability;

	public string weapon;
	public string hat;
	public string top;
	public string gloves;
	public string pants;
	public string shoes;
	public string necklace;
	public string earring_one;
	public string earring_two;
	public string ring_one;
	public string ring_two;
}

