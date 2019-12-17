using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataManager : Singletone<SkillDataManager>
{
	public SkillSOManager dataSO;

	public struct SkillInfo
	{
		public string skillName;
		public float collisionSize;
		public float damageRatio;
		public Vector2 damageForce;
		public Sprite skillImage;
		public float coolTime;
	}

	public SkillInfo GetSkillInfo(string _skillName)
	{
		SkillInfo skillInfo;

		dataSO = dataSO ?? (SkillSOManager)Resources.Load("Data/PlayerSkill/SkillSOManager");

		for (int i = 0; i < dataSO.skillSOList.Count; ++i)
		{
			if(dataSO.skillSOList[i].skillName == _skillName)
			{
				skillInfo.skillName = dataSO.skillSOList[i].skillName;
				skillInfo.collisionSize = dataSO.skillSOList[i].collisionSize;
				skillInfo.damageRatio = dataSO.skillSOList[i].damageRatio;
				skillInfo.damageForce = dataSO.skillSOList[i].damageForce;
				skillInfo.skillImage = dataSO.skillSOList[i].skillImage;
				skillInfo.coolTime = dataSO.skillSOList[i].coolTime;
				return skillInfo;
			}
		}

		skillInfo.skillName = null;
		skillInfo.collisionSize = 0;
		skillInfo.damageRatio = 0;
		skillInfo.damageForce = Vector2.zero;
		skillInfo.skillImage = null;
		skillInfo.coolTime = 0;

		return skillInfo;
	}
}
