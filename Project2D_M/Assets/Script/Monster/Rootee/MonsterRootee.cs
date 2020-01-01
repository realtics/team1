using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRootee : MonsterFsmBase
{
	struct AttackInfo
	{
		public float damageRatio;
		public Vector2 damageForce;
		public AttackInfo(float _damageRatio, Vector2 _damageForce = default(Vector2))
		{
			damageRatio = _damageRatio;

			if (_damageForce == Vector2.zero)
				damageForce = Vector2.zero;
			else damageForce = _damageForce;
		}
	}

	private Dictionary<string, AttackInfo> m_normalAttackDic;
	private AttackCollider m_attackcollider = null;
	private bool m_bAttacking;
	private const float m_fAttackDelay = 2.0f;
	private readonly int m_hashiAttackType = Animator.StringToHash("iAttackType");
	private RooteeSkill m_rootSkill = null;

	ATTACK_KINDS m_eAttack;

	private enum ATTACK_KINDS
	{
		ATTACK_1 = 1,
		ATTACK_2,
		ATTACK_3
	}

	private void Awake()
	{
		m_attackcollider = this.GetComponentInChildren<AttackCollider>();
		m_bAttacking = false;
		m_normalAttackDic = new Dictionary<string, AttackInfo>();
		m_normalAttackDic.Add(ATTACK_KINDS.ATTACK_1.ToString(), new AttackInfo(1.0f, new Vector2(2.0f, 10.0f)));
		m_normalAttackDic.Add(ATTACK_KINDS.ATTACK_2.ToString(), new AttackInfo(1.0f, new Vector2(3.0f, 10.0f)));
		m_normalAttackDic.Add(ATTACK_KINDS.ATTACK_3.ToString(), new AttackInfo(1.0f, new Vector2(3.0f, 10.0f)));
		m_currentDelay = 0;
		m_rootSkill = GetComponentInChildren<RooteeSkill>();
		m_rootSkill.InitSkill();
		InitMonstInfo();
	}
	protected override IEnumerator Attack()
	{
		MoveStop();
		while (true)
		{
			m_currentDelay -= Time.deltaTime;
			CheckHit();
			CheckDie();
			if (!m_animator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
			{
				m_bAttacking = false;
			}
			if (!m_bAttacking && !m_bIsAir)
			{
				m_monsterHpBar.SetHpBarDirection(this.transform.localScale.x);

				if (m_currentDelay < 0)
				{
					m_bRangedAttack = false;
					if (CheckCanAttack())
					{
						RandomAttack();
						m_bAttacking = true;
						m_currentDelay = m_fAttackDelay;
					}
					else
					{
						nowState = ENEMY_STATE.MOVE;
					}
				}
			}
			yield return null;
		}
	}

	private void RandomAttack()
	{
		int random;
		random = Random.Range(1, 40);

		if (random % 2 == 0 && m_bRangedAttack)
		{
			m_eAttack = ATTACK_KINDS.ATTACK_3;
			Invoke("SkillEventOfRootee", 1.5f);
		}
		else if (m_bRangedAttack)
		{
			m_eAttack = ATTACK_KINDS.ATTACK_1;
			m_monsterMove.isMove = true;
			Invoke("SkillEventMove", 1f);
		}
		else
		{
			m_eAttack = ATTACK_KINDS.ATTACK_2;
			//Invoke("SkillEventOfRootee", 1.5f);
		}

		//m_eAttack에 따라 그것에 맞는 공격/스킬이 나감(애니메이션 연계도)
		m_attackcollider.SetDamageColliderInfo(m_normalAttackDic[m_eAttack.ToString()].damageRatio,
			"Player", m_normalAttackDic[m_eAttack.ToString()].damageForce);
		m_animator.SetInteger(m_hashiAttackType, (int)m_eAttack);
		m_animator.SetTrigger("tAttack");
	}

	public void SkillEventOfRootee()
	{
		float tempDir = this.transform.localScale.x;
		
		if(tempDir<0)
		{
			m_rootSkill.SkillAction(true, (int)(m_monsterInfo.attack *2.3f));
		}
		else
		{
			m_rootSkill.SkillAction(false, (int)(m_monsterInfo.attack * 2.3f));
		}
	}

	public void SkillEventMove()
	{
		m_monsterMove.isMove = false;
	}
}
