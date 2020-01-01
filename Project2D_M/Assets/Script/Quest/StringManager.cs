using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringManager : MonoBehaviour
{
	Dictionary<NPC_TYPE, string[]> m_talkData;
	Dictionary<NPC_TYPE, GameObject> m_portraitData;

	public GameObject[] portraitArray;

	private void Awake()
	{
		m_talkData = new Dictionary<NPC_TYPE, string[]>();
		m_portraitData = new Dictionary<NPC_TYPE, GameObject>();
		GenerateData();
	}

	private void GenerateData()
	{
		m_talkData.Add(NPC_TYPE.NPC_MARI, new string[] { "어서오세요", "이곳에 처음 오셨군요?" });

		m_portraitData.Add(NPC_TYPE.NPC_MARI, portraitArray[0]);


		//퀘스트 Talk
		m_talkData.Add(10 + NPC_TYPE.NPC_MARI, new string[] { "새로운 모험가님이시군요! ", "저는 앞으로 당신을 부려먹을 npc랍니다." });

		m_talkData.Add(11 + NPC_TYPE.NPC_MARI, new string[] { "그럼 갑작스럽지만 에페노바 숲에서 저녁거릴 구해다 오실 수 있을까요?" });
	}

	public string GetTalk(NPC_TYPE id, int talkindex)
	{
		if (talkindex == m_talkData[id].Length)
			return null;
		else
			return m_talkData[id][talkindex];
	}

	public GameObject GetPortrait(NPC_TYPE id)
	{
		return m_portraitData[id];
	}


}
