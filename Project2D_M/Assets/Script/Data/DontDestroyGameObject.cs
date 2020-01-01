using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSaveData;
using GameSaveDataIO;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_25
 * 팀              : 1팀
 * 스크립트 용도   : 게임 데이터를 묶어서 관리 예정인 스크립트
 */
public class DontDestroyGameObject : MonoBehaviour
{
	private void Awake()
    {
		if (GameObject.FindGameObjectsWithTag("DataManager").Length <= 1)
			DontDestroyOnLoad(gameObject);
		else Destroy(this.gameObject);
    }
}