using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SkillFuntion : MonoBehaviour
{
	public string sSkillName;
	public float collisionSize = 1f;
	public float damageRatio = 30.0f;
	public Vector2 damageForce = new Vector2(0, -10f);

	protected PlayerState m_playerState;
	protected PlayerAnimFuntion m_animFuntion;
	protected Rigidbody2D m_rigidbody2D;

	public virtual void InitSkill(PlayerAnimFuntion _animFuntion, PlayerState _playerState)
	{
		m_animFuntion = _animFuntion;
		m_playerState = _playerState;
		m_rigidbody2D = this.transform.root.GetComponent<Rigidbody2D>();
	}

	public virtual void SkillAction()
	{
		string sSkillEffentName;

		if (m_playerState.IsPlayerGround())
			sSkillEffentName = sSkillName + "_ground";
		else
		{
			sSkillEffentName = sSkillName + "_air";
		}

		StartCoroutine(nameof(SkillCoroutine));
		m_animFuntion.PlayAnim(sSkillEffentName);
	}

	protected IEnumerator SkillCoroutine()
	{
		m_playerState.bSkipEvasion = false;

		m_playerState.PlayerStateSPAttack();

		m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;

		yield return new WaitForSeconds(0.01f);

		while (m_animFuntion.IsTag(sSkillName))
		{
			yield return new WaitForSeconds(0.01f);
		}

		m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
		m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, -0.01f);

		if (!m_playerState.IsPlayerGround())
			m_playerState.PlayerStateDoubleJump();

		m_playerState.bSkipEvasion = true;
	}
}