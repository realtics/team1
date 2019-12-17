﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLupe : MonsterFsmBase
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


	LUPE_ATTACK m_eAttack;
	//public bool m_bAttack;


	private enum LUPE_ATTACK
	{
		ATTACK_1 = 1,
		ATTACK_2
	}

	private void Awake()
	{
		m_attackcollider = this.GetComponentInChildren<AttackCollider>();
		m_bAttacking = false;
		m_normalAttackDic = new Dictionary<string, AttackInfo>();
		m_normalAttackDic.Add(LUPE_ATTACK.ATTACK_1.ToString(), new AttackInfo(1.0f, new Vector2(2.0f, 10.0f)));
		m_normalAttackDic.Add(LUPE_ATTACK.ATTACK_2.ToString(), new AttackInfo(1.0f, new Vector2(3.0f, 10.0f)));

		m_currentDelay = 0;

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

					if (CheckCanAttack())
					{
						RandomAttack();
						m_bAttacking = true;
					}
					else
					{
						nowState = ENEMY_STATE.MOVE;
					}
					m_currentDelay = m_fAttackDelay;
				}
			}
			yield return null;
		}
	}

	private void RandomAttack()
	{
		int random;
		random = Random.Range(1, 40);

		if (random % 4 == 0)
		{
			m_eAttack = LUPE_ATTACK.ATTACK_1;
		}
		else
		{
			m_eAttack = LUPE_ATTACK.ATTACK_1;
		}

		//m_eAttack에 따라 그것에 맞는 공격/스킬이 나감(애니메이션 연계도)
		m_attackcollider.SetDamageColliderInfo(m_normalAttackDic[m_eAttack.ToString()].damageRatio,
			"Player", m_normalAttackDic[m_eAttack.ToString()].damageForce);
		m_animator.SetInteger(m_hashiAttackType, (int)m_eAttack);
		m_animator.SetTrigger("tAttack");
	}
}
