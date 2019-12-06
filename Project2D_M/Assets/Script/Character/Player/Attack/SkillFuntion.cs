using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SkillFuntion : MonoBehaviour
{
	public string sSkillName = "FlameHaze";
	public float iCollisionSize = 0.5f;
	private float iDamageRatio = 10000000.0f;
	private Vector2 iDamageForce = new Vector2(0, -50f);


	private SkeletonAnimation m_skeletonAnimation;
	private PlayerState m_playerState;
	private MeshRenderer m_meshRenderer;
	private PlayerAnimFuntion m_animFuntion;
	private AttackCollider m_attackCollider;
	private Rigidbody2D m_rigidbody2D;

	public void InitSkill(PlayerAnimFuntion _animFuntion, PlayerState _playerState)
	{
		m_animFuntion = _animFuntion;
		m_playerState = _playerState;

		m_skeletonAnimation = this.GetComponent<SkeletonAnimation>();
		m_meshRenderer = this.GetComponent<MeshRenderer>();
		m_attackCollider = this.GetComponent<AttackCollider>();
		m_rigidbody2D = this.transform.root.GetComponent<Rigidbody2D>();

		m_attackCollider.iCollisionSize = iCollisionSize;
		m_attackCollider.SetDamageColliderInfo(iDamageRatio, "Monster", iDamageForce);
	}

	public void SkillAction()
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

	IEnumerator SkillCoroutine()
	{
		m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;

		yield return new WaitForSeconds(0.01f);

		while (m_animFuntion.IsTag(sSkillName))
		{
			yield return new WaitForSeconds(0.01f);
		}

		m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
		m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, -0.01f);
	}
}