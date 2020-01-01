using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : Singletone<QuestManager>
{
	//관리
	public StringManager talkManager;
	public Quest quest;

	//대화창
	public GameObject talkPanel;
	public TextMeshProUGUI talkText;
	public GameObject talkMari;
	public GameObject talkHero;

	//NPC
	public GameObject talkObject;

	//변수
	public bool isClick;
	public int talkIndex;

	private void Awake()
	{
		if(talkManager == null)
		{

		}
	}

	public void TalkClick(GameObject _talkObject)
	{
		talkObject = _talkObject;
		NpcData npcData = talkObject.GetComponent<NpcData>();
		Talk(npcData.id);

	}

	void Talk(NPC_TYPE _npcId)
	{
		int questTalkIndex = quest.GetQuestTalkIndex(_npcId);
		string talkData =  talkManager.GetTalk(_npcId + questTalkIndex, talkIndex);

		//대화 끝날 때
		if (talkData == null)
		{
			isClick = false;
			talkIndex = 0;
			return;
		}

		talkText.text = talkData;

		isClick = true;
		talkIndex++;
	}
}
