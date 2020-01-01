using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLarvo : MonsterFsmBase
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
    
    //패턴상태 상위 어택 코루틴을 돌지 않게 하기위한 불문
	private bool m_patternPlaying;

	[SerializeField]
	GameObject m_effect;

	ATTACK_KINDS m_eAttack;
	//public bool m_bAttack;


	private enum ATTACK_KINDS
	{
		ATTACK_1 = 1,
		ATTACK_2
	}

	protected override void Start()
	{
		m_monsterKind = MONSTER_KINDS.BOSS_LARVO;
		m_patternPlaying = false;
		m_attackcollider = this.GetComponentInChildren<AttackCollider>();
		m_bAttacking = false;
		m_normalAttackDic = new Dictionary<string, AttackInfo>();
		m_normalAttackDic.Add(ATTACK_KINDS.ATTACK_1.ToString(), new AttackInfo(1.0f, new Vector2(2.0f, 1.0f)));
        //m_normalAttackDic.Add(ATTACK_KINDS.ATTACK_2.ToString(), new AttackInfo(1.0f, new Vector2(3.0f, 10.0f)));

        m_currentDelay = 0;

		InitMonstInfo();
		base.Start();
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
				if (m_currentDelay < 0)
				{

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

		if (random % 8 == 0)
		{
			m_eAttack = ATTACK_KINDS.ATTACK_1;
		}
		else
		{
			m_eAttack = ATTACK_KINDS.ATTACK_1;
			StartCoroutine(PlayAttackEffect());
		}

		//m_eAttack에 따라 그것에 맞는 공격/스킬이 나감(애니메이션 연계도)
		m_attackcollider.SetDamageColliderInfo(m_normalAttackDic[m_eAttack.ToString()].damageRatio,
			"Player", m_normalAttackDic[m_eAttack.ToString()].damageForce);
		m_animator.SetInteger(m_hashiAttackType, (int)m_eAttack);
		m_animator.SetTrigger("tAttack");
	}

    IEnumerator Pattern1()
	{
		while(true)
		{

			yield return null;
		}
	}

	IEnumerator PlayAttackEffect()
	{
		float time = 0.8f;
		//m_effect.SetActive(true);

		while (true)
		{
			time -= Time.deltaTime;
			if (time < 0.2 && time> 0 && m_effect.activeSelf==false)
			{
				m_monsterMove.Move(10);

				m_effect.SetActive(true);
			}
			if(time<-0.1)
			{
				m_effect.SetActive(false);
				//m_monsterMove.isMove = false;
				time = 0.8f;
				break;
			}
			yield return null;
		}
	}
	protected override IEnumerator Appear()
	{

		while (true)
		{
			m_appearTime -= Time.deltaTime;
			if (m_appearTime < 0)
			{
                if (WorkUpdateHpBar == null)
                {
                    WorkUpdateHpBar = new Work(CheckHP(), true);
                }
                m_animator.speed = 1.0f;
				m_animator.SetBool(m_hashBAppear, true);
				nowState = ENEMY_STATE.IDLE;
			}
			yield return null;
		}
	}
}
