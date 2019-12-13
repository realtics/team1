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
		m_animFuntion = _animFuntion;
		m_playerState = _playerState;

		m_skeletonAnimation = this.GetComponent<SkeletonAnimation>();
		m_meshRenderer = this.GetComponent<MeshRenderer>();
		m_attackCollider = this.GetComponent<AttackCollider>();
		m_rigidbody2D = this.transform.root.GetComponent<Rigidbody2D>();

		m_attackCollider.iCollisionSize = collisionSize;
		m_attackCollider.SetDamageColliderInfo(damageRatio, "Monster", damageForce);
	}

	public override void SkillAction()
	{
		if (m_meshRenderer.enabled == false)
			m_meshRenderer.enabled = true;

		string sSkillEffentName;

		if (m_playerState.IsPlayerGround())
			sSkillEffentName = sSkillName + "_ground";
		else
		{
			sSkillEffentName = sSkillName + "_air";
		}

		StartCoroutine(nameof(SkillCoroutine));
		m_skeletonAnimation.AnimationState.SetAnimation(0, sSkillEffentName, false);
		m_animFuntion.PlayAnim(sSkillEffentName);
	}
}
