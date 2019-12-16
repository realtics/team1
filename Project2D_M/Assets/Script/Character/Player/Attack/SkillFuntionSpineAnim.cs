using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SkillFuntionSpineAnim : SkillFuntion
{
	protected SkeletonAnimation m_skeletonAnimation;
	protected MeshRenderer m_meshRenderer;
	protected AttackCollider m_attackCollider;

	public override void InitSkill(PlayerAnimFuntion _animFuntion, PlayerState _playerState)
	{
		base.InitSkill(_animFuntion, _playerState);

		m_skeletonAnimation = this.GetComponent<SkeletonAnimation>();
		m_meshRenderer = this.GetComponent<MeshRenderer>();
		m_attackCollider = this.GetComponent<AttackCollider>();

		m_attackCollider.iCollisionSize = collisionSize;
		m_attackCollider.SetDamageColliderInfo(damageRatio * level, "Monster", damageForce);
	}

	public override void SkillAction()
	{
		string skillEffentName;

		if (m_playerState.IsPlayerGround())
			skillEffentName = skillName + "_ground";
		else
		{
			skillEffentName = skillName + "_air";
		}

		StartCoroutine(nameof(SkillCoroutine), skillEffentName);
		m_skeletonAnimation.AnimationState.SetAnimation(0, skillEffentName, false);
		m_animFuntion.PlayAnim(skillEffentName);
	}

	protected override IEnumerator SkillCoroutine(string _skillEffentName)
	{
		if (m_meshRenderer.enabled == false)
			m_meshRenderer.enabled = true;

		m_playerState.bSkipAction = false;

		m_playerState.PlayerStateSPAttack();

		m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;

		yield return new WaitForSeconds(0.01f);

		while (m_animFuntion.IsTag(skillName))
		{
			yield return new WaitForSeconds(0.01f);
		}

		m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
		m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, -0.01f);

		if (!m_playerState.IsPlayerGround())
			m_playerState.PlayerStateDoubleJump();

		m_playerState.bSkipAction = true;

		if (m_meshRenderer.enabled == true)
			m_meshRenderer.enabled = false;
	}
}
