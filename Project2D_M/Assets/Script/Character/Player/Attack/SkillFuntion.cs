using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SkillFuntion : MonoBehaviour
{
	public int iSkillNum;
	public string sSkillName;
	public string sSkillClipName = "skill3_flame_haze_2_action";
	public float iCollisionSize = 0.5f;
	private string sSkillEffentName = "FlameHaze_ground";
	private float iDamageRatio = 10000000.0f;
	private Vector2 iDamageForce = new Vector2(0,-50f);
	private SkeletonAnimation m_skeletonAnimation;
	private MeshRenderer m_meshRenderer;
	private AnimFuntion m_animFuntion;
	private AttackCollider m_attackCollider;

	public void InitSkill(AnimFuntion _animFuntion)
	{
		m_animFuntion = _animFuntion;
		m_skeletonAnimation = this.GetComponent<SkeletonAnimation>();
		m_meshRenderer = this.GetComponent<MeshRenderer>();
		m_attackCollider = this.GetComponent<AttackCollider>();

		m_attackCollider.iCollisionSize = iCollisionSize;
		m_attackCollider.SetDamageColliderInfo(iDamageRatio, "Monster", iDamageForce);
	}

	public void SkillAction()
	{
		if(m_meshRenderer.enabled == false)
			m_meshRenderer.enabled = true;
		m_skeletonAnimation.AnimationState.SetAnimation(0, sSkillEffentName, false);
		m_animFuntion.PlayAnim(sSkillEffentName);
	}
}
