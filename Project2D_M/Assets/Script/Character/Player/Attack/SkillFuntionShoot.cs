using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFuntionShoot : SkillFuntion
{
	protected ISkillShoot m_skillShoot;
	protected CharacterInfo m_characterInfo;
	protected DamageInfo m_damageInfo;

	[SerializeField] protected GameObject shotObject = null;
	[SerializeField] protected int objectCount;
	public override void InitSkill(PlayerAnimFuntion _animFuntion, GameObject _playerObject)
	{
		base.InitSkill(_animFuntion, _playerObject);

		m_characterInfo = _playerObject.GetComponent<CharacterInfo>();
		m_skillShoot = GetComponent<ISkillShoot>();

		m_damageInfo.damage = (int)((m_characterInfo.attack * (damageRatio * level)) + 0.5f);

		ObjectPool.Inst.Initialize(shotObject, objectCount);

		m_damageInfo.attackForce = damageForce;
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
		m_playerState.bSkipAction = false;

		m_animFuntion.PlayAnim(_skillEffentName);

		yield return new WaitForSeconds(0.03f);

		m_playerState.PlayerStateSPAttack();
		m_rigidbody2D.velocity = Vector3.zero;
		m_crowdControlManager.OnAirStop();
		m_crowdControlManager.SuperArmorOn();

		string currntAnimName = m_animFuntion.GetCurrntAnimClipName();

        m_skillShoot.InitShoot(m_playerState.IsPlayerLookRight(), m_damageInfo);

        while (m_animFuntion.IsTag(skillName))
		{
			if(currntAnimName != m_animFuntion.GetCurrntAnimClipName())
			{
				currntAnimName = m_animFuntion.GetCurrntAnimClipName();
				ChangeAnim(_skillEffentName);
			}
			yield return new WaitForSeconds(0.01f);
		}

		m_crowdControlManager.OffAirStop();
		m_crowdControlManager.SuperArmorOff();

		if (!m_playerState.IsPlayerGround())
			m_playerState.PlayerStateDoubleJump();

		m_playerState.bSkipAction = true;
	}

	protected virtual void ChangeAnim(string _skillEffentName)
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