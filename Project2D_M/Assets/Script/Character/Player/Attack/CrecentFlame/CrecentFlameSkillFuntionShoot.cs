using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrecentFlameSkillFuntionShoot : SkillFuntionShoot
{
    public override bool SkillAction()
	{
		string skillEffentName;

		if (m_playerState.IsPlayerGround())
		{
			skillEffentName = skillName + "_ground";
			StartCoroutine(nameof(SkillCoroutine), skillEffentName);
			return true;
		}

		return false;
	}

	protected override IEnumerator SkillCoroutine(string _skillEffentName)
	{
		m_playerState.bSkipAction = false;


		m_animFuntion.PlayAnim(_skillEffentName);
		m_crowdControlManager.ImpenetrableOn();

		yield return new WaitForSeconds(0.03f);

		m_playerState.PlayerStateSPAttack();
		m_rigidbody2D.velocity = Vector3.zero;
		m_crowdControlManager.OnAirStop();
		string currntAnimName = m_animFuntion.GetCurrntAnimClipName();

        m_skillShoot.InitShoot(m_playerState.IsPlayerLookRight(), m_damageInfo);

        while (m_animFuntion.IsTag(skillName))
		{
			if (currntAnimName != m_animFuntion.GetCurrntAnimClipName())
			{
				currntAnimName = m_animFuntion.GetCurrntAnimClipName();
				ChangeAnim(_skillEffentName);
			}
			yield return new WaitForSeconds(0.01f);
		}

		m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
		m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, -0.01f);

		m_crowdControlManager.OffAirStop();

		if (!m_playerState.IsPlayerGround())
			m_playerState.PlayerStateDoubleJump();

		m_crowdControlManager.ImpenetrableOff();
		m_playerState.bSkipAction = true;
	}

	protected override void ChangeAnim(string _skillEffentName)
	{
		if (m_animFuntion.IsTag(skillName))
		{
			if (m_animFuntion.IsName(_skillEffentName + "_action"))
			{
				m_skillShoot.ShootAction();
			}
		}
	}
}
