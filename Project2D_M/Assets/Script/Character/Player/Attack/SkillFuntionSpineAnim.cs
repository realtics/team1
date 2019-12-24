using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SkillFuntionSpineAnim : SkillFuntion
{
	protected MeshRenderer m_meshRenderer;
	protected AttackCollider m_attackCollider;
	protected EffectAnimFuntion m_effectAnimFuntion;
	public override void InitSkill(PlayerAnimFuntion _animFuntion, GameObject _playerObject)
	{
		base.InitSkill(_animFuntion, _playerObject);

		m_meshRenderer = this.GetComponent<MeshRenderer>();
		m_attackCollider = this.GetComponent<AttackCollider>();
		m_effectAnimFuntion = this.GetComponent<EffectAnimFuntion>();

		m_attackCollider.iCollisionSize = collisionSize;
		m_attackCollider.SetDamageColliderInfo(damageRatio * level, "Monster", damageForce);
	}

	public override bool SkillAction()
	{
		string skillEffentName;

		if (m_playerState.IsPlayerGround())
			skillEffentName = skillName + "_ground";
		else
		{
			skillEffentName = skillName + "_air";
		}

		StartCoroutine(nameof(SkillCoroutine), skillEffentName);

		return true;
	}

	protected override IEnumerator SkillCoroutine(string _skillEffentName)
	{
		if (m_meshRenderer.enabled == false)
			m_meshRenderer.enabled = true;

		m_playerState.bSkipAction = false;

		m_effectAnimFuntion.EffectPlay(_skillEffentName, false);
		m_animFuntion.PlayAnim(_skillEffentName);

		yield return new WaitForSeconds(0.03f);

		m_playerState.PlayerStateSPAttack();
		m_rigidbody2D.velocity = Vector3.zero;
		m_crowdControlManager.OnAirStop();
		m_crowdControlManager.SuperArmorOn();

		while (m_animFuntion.IsTag(skillName))
		{
			yield return new WaitForSeconds(0.01f);
		}

		m_crowdControlManager.OffAirStop();
		m_crowdControlManager.SuperArmorOff();

		if (!m_playerState.IsPlayerGround())
			m_playerState.PlayerStateDoubleJump();

		m_playerState.bSkipAction = true;

		if (m_meshRenderer.enabled == true)
			m_meshRenderer.enabled = false;
	}
}
