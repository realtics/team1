using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterFsmBase : MonsterStateMachine
{
	private readonly int m_hashFSpeed = Animator.StringToHash("fSpeed");
	private readonly int m_hashBLive = Animator.StringToHash("bLive");
	private readonly int m_hashBStun = Animator.StringToHash("bStun");
	private readonly int m_hashBAppear = Animator.StringToHash("bAppear");
	private readonly int m_hashTHit = Animator.StringToHash("tHit");
	private readonly int m_hashTAttack = Animator.StringToHash("tAttack");

	protected Animator m_animator;
    private MonsterMove m_monsterMove;
	private MonsterInfo m_monsterInfo;
	private MonsterAttack m_monsterAttack;

    [SerializeField]
	private float m_appearTime;

	protected bool m_bIsAir;
	protected float m_currentDelay;
	private bool m_bLive;

	protected MonsterHpBar m_monsterHpBar;
	
	#region Work Kind
	private Work WorkAppear;
	private Work WorkIdle;
	private Work WorkMove;
	private Work WorkHit;
	private Work WorkAttack;
	private Work WorkDie;
	#endregion;

	void Start()
    {
		InitAniamation();
		nowState = ENEMY_STATE.APPEAR;
		//m_AppearTime;

		#region value bool
		m_bIsAir = false;
		#endregion
		m_monsterHpBar = GetComponentInChildren<MonsterHpBar>();
		m_monsterHpBar.transform.gameObject.SetActive(false);

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
				//ActionStart(Idle());
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
				m_animator.SetBool(m_hashBAppear, true);
                nowState = ENEMY_STATE.IDLE;
				m_monsterHpBar.transform.gameObject.SetActive(true);
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
			m_monsterAttack.m_bAttack = true;
			yield return null;
		}
	}

    IEnumerator Move()
    {
		while (true)
        {
			Debug_UI.Inst.SetDebugText("Cha_anim", m_animFunction.GetCurrntAnimClipName().ToString());
			Debug_UI.Inst.SetDebugText("isMove", m_monsterMove.isMove.ToString());
			Debug_UI.Inst.SetDebugText("isAir", m_bIsAir.ToString());
			CheckHit();
			CheckCanAttack();
			if (!m_bIsAir)
			{
				m_monsterMove.isMove = true;
				m_animator.SetFloat(m_hashFSpeed, m_fSpeed);
				m_monsterHpBar.SetHpBarDirection(this.transform.localScale.x);
			}
			yield return null;
		}
	}

	IEnumerator Hit()
	{
		float time = 0.7f;
		m_monsterMove.isMove = false;
		m_animator.SetFloat(m_hashFSpeed, 0);
		m_monsterMove.m_characterMove.MoveStop();


		while (true)
		{
			///Debug
			Debug_UI.Inst.SetDebugText("Cha_anim", m_animFunction.GetCurrntAnimClipName().ToString());
			Debug_UI.Inst.SetDebugText("isMove", m_monsterMove.isMove.ToString());
			Debug_UI.Inst.SetDebugText("isAir", m_bIsAir.ToString());
			///Debug End

			m_monsterHpBar.SetHPBar(m_monsterInfo);
			m_monsterHpBar.SetHpBarDirection(this.transform.localScale.x);
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

	IEnumerator Die()
	{
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
	#endregion



	#region function
	protected void InitAniamation()
	{
		m_animator = this.GetComponentInChildren<Animator>();
		m_animator.SetBool(m_hashBLive, true);
		m_animFunction = transform.GetComponentInChildren<AnimFuntion>();
	}

	protected bool CheckCanAttack()
	{
		if (StageManager.Inst.playerTransform.position.x - this.transform.position.x > - m_monsterInfo.GetAttackDistance() &&
			StageManager.Inst.playerTransform.position.x - this.transform.position.x < m_monsterInfo.GetAttackDistance())
		{
			if(nowState ==ENEMY_STATE.ATTACK)
			{
			}
			else
			{
				nowState = ENEMY_STATE.ATTACK;
			}
			m_monsterMove.MoveDir();
			return true;
		}
		return false;
	}

	AnimFuntion m_animFunction;
	protected void CheckHit()
	{
		if (m_animFunction.GetCurrntAnimClipName() == "hit")
		{
			//m_monsterHpBar.SetHPBar(m_monsterInfo);
			//m_monsterHpBar.SetHpBarDirection(this.transform.localScale.x);
			nowState = ENEMY_STATE.HIT;
		}
	}

	protected void CheckDie()
	{
		if(m_monsterInfo.IsCharacterDie())
		{
			m_monsterHpBar.SetHPBar(m_monsterInfo);
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
