using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
	public int questId;
	public int questActionIndex; // 퀘스트 대화순서

	Dictionary<int, QuestData> questDic;

	private void Awake()
	{
		questDic = new Dictionary<int, QuestData>();
		GenerateData();
	}

	void GenerateData()
	{
		questDic.Add(10, new QuestData("시작되는 모험", new NPC_TYPE[] { NPC_TYPE.NPC_MARI, NPC_TYPE.NPC_MARI }));

		questDic.Add(20, new QuestData("두근두근 모험", new NPC_TYPE[] { NPC_TYPE.NPC_MARI }));
	}

	public int GetQuestTalkIndex(NPC_TYPE _id)
	{
		return questId + questActionIndex;
	}

	public string CheckQuest(NPC_TYPE _id)
	{
		if(_id == questDic[questId].npcId[questActionIndex])
		{
			questActionIndex++;
		}

		if(questActionIndex == questDic[questId].npcId.Length)
		{
			NextQuest();
		}

		return questDic[questId].questName;
	}

	void NextQuest()
	{
		questId += 10;
		questActionIndex = 0;
	}
}
