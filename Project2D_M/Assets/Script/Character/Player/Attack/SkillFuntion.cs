using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SkillFuntion : MonoBehaviour
{
	public string skillName;
	public float collisionSize;
	public float damageRatio;
	public Vector2 damageForce;
	public int level = 1;

	protected PlayerState m_playerState;
	protected PlayerAnimFuntion m_animFuntion;
	protected Rigidbody2D m_rigidbody2D;

	public virtual void InitSkill(PlayerAnimFuntion _animFuntion, PlayerState _playerState)
	{
		SkillDataManager.SkillInfo skillInfo = SkillDataManager.Inst.GetSkillInfo(gameObject.name );
		skillName = skillInfo.skillName;
		collisionSize = skillInfo.collisionSize;
		damageRatio = skillInfo.damageRatio;
		damageForce = skillInfo.damageForce;

		m_animFuntion = _animFuntion;
		m_playerState = _playerState;
		m_rigidbody2D = this.transform.root.GetComponent<Rigidbody2D>();
	}

	public virtual void SkillAction()
	{
		
	}

	protected virtual IEnumerator SkillCoroutine(string _skillEffentName)
	{
		yield break;
	}
}