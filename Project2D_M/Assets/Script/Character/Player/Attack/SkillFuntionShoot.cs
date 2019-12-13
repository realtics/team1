using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFuntionShoot : SkillFuntion
{
    private ISkillShoot m_skillShoot;
	private CharacterInfo m_characterInfo;
	private DamageInfo m_damageInfo;
	public override void InitSkill(PlayerAnimFuntion _animFuntion, PlayerState _playerState)
	{
		m_animFuntion = _animFuntion;
		m_playerState = _playerState;
		m_rigidbody2D = this.transform.root.GetComponent<Rigidbody2D>();
		m_characterInfo = this.transform.root.GetComponent<CharacterInfo>();

		m_skillShoot = GetComponent<ISkillShoot>();

		m_damageInfo.damage = (int)((m_characterInfo.attack * damageRatio) + 0.5f);

		if ((this.transform.root.transform.localScale.x > 0 && damageForce.x < 0) ||
			(this.transform.root.transform.localScale.x < 0 && damageForce.x > 0))
		{
			damageForce.x = damageForce.x * -1;
		}

		m_damageInfo.attackForce = damageForce;
	}

	public override void SkillAction()
	{
		base.SkillAction();

		m_skillShoot.ShootAction(m_playerState.IsPlayerLookRight(), m_damageInfo);
	}
}