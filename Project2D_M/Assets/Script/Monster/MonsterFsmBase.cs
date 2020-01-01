using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFsmBase : MonsterStateMachine
{
	protected readonly int m_hashFSpeed = Animator.StringToHash("fSpeed");
	protected readonly int m_hashBLive = Animator.StringToHash("bLive");
	protected readonly int m_hashBStun = Animator.StringToHash("bStun");
	protected readonly int m_hashBAppear = Animator.StringToHash("bAppear");
	protected readonly int m_hashTHit = Animator.StringToHash("tHit");
	protected readonly int m_hashTAttack = Animator.StringToHash("tAttack");

	protected Animator m_animator;
    protected MonsterMove m_monsterMove;
	protected MonsterInfo m_monsterInfo;
	protected CrowdControlManager m_crowdControlMg;


    [SerializeField]
	protected float m_appearTime;
	[SerializeField]
	private int m_attackRanged;
    private bool m_bDie;

	protected bool m_bIsAir;
	protected float m_currentDelay;
	private bool m_bLive;
	protected bool m_bRangedMonster;
	protected bool m_bRangedAttack;

	protected MonsterHpBar m_monsterHpBar;
    protected BossMonsterHpBar m_bossHpBar;

    #region Work Kind
    private Work WorkAppear;
	private Work WorkIdle;
	private Work WorkMove;
	private Work WorkHit;
	private Work WorkAttack;
	private Work WorkDie;
    protected Work WorkUpdateHpBar;
	private Work WorkStun;
	#endregion;

	protected enum MONSTER_KINDS
	{
		MELEE_LUPE,
		RANGED_ROOTEE,
		BOSS_LARVO
	}

    protected enum MONSTER_RANK
    {
        MONSTER_NORMAL,
        MONSTER_BOSS
    }

	protected MONSTER_KINDS m_monsterKind;
    [SerializeField]
    protected MONSTER_RANK m_monsterRank;

	protected virtual void Start()
    {
		InitAniamation();
		nowState = ENEMY_STATE.APPEAR;
        m_bDie = false;


        if (m_monsterKind == MONSTER_KINDS.BOSS_LARVO)
		{
			m_animator.speed = 0.0f;
		}

		#region value bool
		m_bIsAir = false;
		m_bRangedMonster = false;
		m_bRangedAttack = false;
        #endregion

        m_bossHpBar = GetComponentInChildren<BossMonsterHpBar>();
        if (m_bossHpBar != null)
            m_bossHpBar.transform.gameObject.SetActive(false);

        m_monsterHpBar = GetComponentInChildren<MonsterHpBar>();
        if (m_monsterHpBar!= null)
            m_monsterHpBar.transform.gameObject.SetActive(false);


        m_crowdControlMg = GetComponent<CrowdControlManager>();
		m_crowdControlMg.Impenetrable(m_appearTime);
		if (this.GetComponent<MonsterRootee>() != null)
			m_bRangedMonster = true;
	}


	override protected void EnterState(ENEMY_STATE _state)
	{
		switch (_state)
		{
			case ENEMY_STATE.APPEAR:
				WorkAppear = new Work(Appear(), true);
				break;
			case ENEMY_STATE.IDLE:
				WorkIdle = new Work(Idle(), true);
				break;
			case ENEMY_STATE.ATTACK:
				WorkAttack = new Work(Attack(), true);
				break;
			case ENEMY_STATE.MOVE:
				WorkMove = new Work(Move(), true);
				break;
			case ENEMY_STATE.DIE:
				WorkDie = new Work(Die(), true);
				break;
			case ENEMY_STATE.HIT:
				WorkHit = new Work(Hit(), true);
				break;
			case ENEMY_STATE.STUN:
				WorkStun = new Work(Stun(), true);
				break;
		}
	}

	protected override void ExitState(ENEMY_STATE _state)
	{
		switch (_state)
		{
			case ENEMY_STATE.APPEAR:
				if(WorkAppear!= null) WorkAppear.KillCoroutine();
				break;
			case ENEMY_STATE.IDLE:
				if(WorkIdle != null)WorkIdle.KillCoroutine();
				break;
			case ENEMY_STATE.ATTACK:
				if (WorkIdle != null) WorkAttack.KillCoroutine();
				break;
			case ENEMY_STATE.MOVE:
				if (WorkMove != null) WorkMove.KillCoroutine();
				break;
			case ENEMY_STATE.DIE:
				//WorkDie 안에 자신의 코루틴을 죽이는 코드가 있음
				break;
			case ENEMY_STATE.HIT:
				if (WorkHit != null) WorkHit.KillCoroutine();
				break;
			case ENEMY_STATE.STUN:
				if (WorkStun != null) WorkStun.KillCoroutine();
				break;
		}
	}

	#region coroutine
	protected virtual IEnumerator Appear()
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
                //m_animator.StartPlayback();
                m_animator.SetBool(m_hashBAppear, true);
                nowState = ENEMY_STATE.IDLE;
				//m_monsterHpBar.transform.gameObject.SetActive(true);
			}
			yield return null;
		}
	}

    IEnumerator Idle()
    {
        float time = 2.0f;
        while(true)
        {
            time -= Time.deltaTime;
			CheckHit();
            if (time < 0)
            {
                nowState = ENEMY_STATE.MOVE;
            }
			yield return null;
		}
	}

	protected virtual IEnumerator Attack()
	{
		while(true)
		{
			//m_monsterAttack.m_bAttack = true;
			yield return null;
		}
	}

    protected virtual IEnumerator Move()
    {
		while (true)
        {
			CheckHit();
			CheckDie();
			CheckCanAttack();
			if (!m_bIsAir)
			{
				m_monsterMove.isMove = true;
				m_animator.SetFloat(m_hashFSpeed, m_fSpeed);
				//m_monsterHpBar.SetHpBarDirection(this.transform.localScale.x);
			}
			yield return null;
		}
	}

    protected virtual IEnumerator Hit()
	{
		float time = 0.7f;
		m_monsterMove.isMove = false;
		m_animator.SetFloat(m_hashFSpeed, 0);
		m_monsterMove.m_characterMove.MoveStop();
		CancelInvoke();

		while (true)
		{
			CheckDie();

			time -= Time.deltaTime;
			if(time<0)
			{
				CheckHit();
				CheckCanAttack();
				nowState = ENEMY_STATE.MOVE;
			}
			yield return null;
		}
	}

    protected virtual IEnumerator Die()
	{
        if(m_bDie== false)
        {
            StageManager.Inst.SetMonsterCount(m_monsterInfo.bOverKill);
            m_bDie = true;
        }
        m_monsterMove.isMove = false;
		float time = 3.0f;
		while(true)
		{
			time -= Time.deltaTime;
			if(time<0)
			{
				WorkDie.KillCoroutine();
				this.gameObject.SetActive(false);
			}
			yield return null;
		}
	}

    protected virtual IEnumerator CheckHP()
    {
        
        if (m_monsterRank== MONSTER_RANK.MONSTER_NORMAL)
        {
            m_monsterHpBar.transform.gameObject.SetActive(true);
            while (true)
            {
                m_monsterHpBar.SetHPBar(m_monsterInfo);
                m_monsterHpBar.SetHpBarDirection(this.transform.localScale.x);
                if (nowState == ENEMY_STATE.DIE)
                    WorkUpdateHpBar.KillCoroutine();
                yield return null;
            }
        }
        else
        {
            m_bossHpBar.transform.gameObject.SetActive(true);
			m_bossHpBar.InitInfo();
            m_bossHpBar.iCurValue = m_monsterInfo.GetHP() / 300;
            m_bossHpBar.SetText();
            m_bossHpBar.iCurValue--;
            while (true)
            {
                //
                float temp = m_monsterInfo.GetHP() / 300;
                temp -= 1;
                if (m_monsterInfo.GetHP() % 300 > 0.0f)
                    temp++;

                if(m_bossHpBar.iCurValue > temp && m_bossHpBar.bLastHp == false)
                {
                    m_bossHpBar.SetHPBar(300, 0);
                    m_bossHpBar.ChangeHpBar();
                    m_bossHpBar.HpZero();
                    m_bossHpBar.ResetHpBar();
                    m_bossHpBar.iCurValue = (int)temp;
                }

                if (temp > -1)
                    m_bossHpBar.SetHPBar(300, m_monsterInfo.GetHP() - temp * 300);
                else
                {
                    m_bossHpBar.SetHPBar(300, 0);
                    m_bossHpBar.HpZero();
                }

                if (m_monsterInfo.GetHP() / 300 == 0)
                    m_bossHpBar.bLastHp = true;

                if (nowState == ENEMY_STATE.DIE)
                {
                    WorkUpdateHpBar.KillCoroutine();
                    m_bossHpBar.EndBossUI();
                }

				m_bossHpBar.SetArmorBar(m_fMaxArmorPoint, m_fArmorPoint);
				CheckArmorBreak();
				yield return null;
            }
        }
    }
	protected virtual IEnumerator Stun()
	{
		m_monsterMove.isMove = false;
		m_animator.SetFloat(m_hashFSpeed, 0);
		float stunTime = 4.0f;
		while(true)
		{
			stunTime -= Time.deltaTime;
			m_bNowArmorBreak = true;
			CheckDie();
			if(stunTime<0)
			{
				m_bNowArmorBreak = false;
				nowState = ENEMY_STATE.MOVE;
			}
			yield return null;
		}
	}

	#endregion

	#region function
	protected void CheckArmorBreak()
	{
		if(m_fArmorPoint<=0)
		{
			m_fArmorPoint = m_fMaxArmorPoint;
			m_bossHpBar.SyncAnimPosition(this.transform);
			m_bossHpBar.PlayAnimArmorBreak();
			nowState = ENEMY_STATE.STUN;
		}
	}
	protected void InitAniamation()
	{
		m_animator = this.GetComponentInChildren<Animator>();
		m_animator.SetBool(m_hashBLive, true);
		m_animFunction = transform.GetComponentInChildren<AnimFuntion>();
	}

	protected bool CheckCanAttack()
	{
		if(m_bRangedMonster)
		{
			if (StageManager.Inst.playerTransform.position.x - this.transform.position.x > -m_monsterInfo.GetAttackDistance() && StageManager.Inst.playerTransform.position.x - this.transform.position.x < m_monsterInfo.GetAttackDistance())
			{
				if (nowState == ENEMY_STATE.ATTACK)
				{
				}
				else
				{
					nowState = ENEMY_STATE.ATTACK;
				}
				m_monsterMove.MoveDir();
				return true;
			}
			else if (StageManager.Inst.playerTransform.position.x - this.transform.position.x > -(m_monsterInfo.GetAttackDistance()+ m_attackRanged) && StageManager.Inst.playerTransform.position.x - this.transform.position.x < (m_monsterInfo.GetAttackDistance()+ m_attackRanged))
			{
				int random;
				random = Random.Range(1, 40);
				m_bRangedAttack = true;

				if (true)
				{
					if (nowState == ENEMY_STATE.ATTACK)
					{
					}
					else
					{
						nowState = ENEMY_STATE.ATTACK;
					}
					m_monsterMove.MoveDir();
					return true;
				}
			}
		}
		else
		{
			if (StageManager.Inst.playerTransform.position.x - this.transform.position.x > -m_monsterInfo.GetAttackDistance() && StageManager.Inst.playerTransform.position.x - this.transform.position.x < m_monsterInfo.GetAttackDistance())
			{
				if (nowState == ENEMY_STATE.ATTACK)
				{
				}
				else
				{
					nowState = ENEMY_STATE.ATTACK;
				}
				m_monsterMove.MoveDir();
				return true;
			}
		}
		return false;
	}

	AnimFuntion m_animFunction;
	protected void CheckHit()
	{
		if (m_animFunction.GetCurrntAnimClipName() == "hit")
		{
			nowState = ENEMY_STATE.HIT;
		}
	}

	protected void CheckDie()
	{
		if(m_monsterInfo.IsCharacterDie())
		{
			//m_monsterHpBar.SetHPBar(m_monsterInfo);
			nowState = ENEMY_STATE.DIE;
			m_animator.SetBool(m_hashBLive, false);
		}
	}

	protected void MoveStop()
	{
		m_monsterMove.isMove = false;
		m_animator.SetFloat(m_hashFSpeed, 0);
	}

	protected void InitMonstInfo()
	{
		//MonsterInfo.MonsterCharInfo monsterCharInfo;
		m_monsterInfo = GetComponent<MonsterInfo>();
		m_monsterMove = GetComponent<MonsterMove>();
		m_monsterMove.SetSpeed(m_monsterInfo.speed);
	}
	#endregion

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
		{
			m_bIsAir = false;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
		{
			m_bIsAir = true;
		}
	}

	#region collider
	private void Oncol (Collider2D collision)
	{
		if(collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
		{
			m_bIsAir = false;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
		{
			m_bIsAir = true;
		}
	}
	#endregion
}
