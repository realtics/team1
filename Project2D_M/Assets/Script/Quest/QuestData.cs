using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
	public string questName;
	public NPC_TYPE[] npcId;

	public QuestData(string _questName, NPC_TYPE[] _npcId)
	{
		questName = _questName;
		npcId = _npcId;
	}
}
