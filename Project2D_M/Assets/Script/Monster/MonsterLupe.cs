using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterLupe : MonsterStateMachine
{
	private readonly int m_hashFSpeed = Animator.StringToHash("fSpeed");
	private readonly int m_hashBLive = Animator.StringToHash("bLive");
	private readonly int m_hashBStun = Animator.StringToHash("bStun");
	private readonly int m_hashBAppear = Animator.StringToHash("bAppear");
	private readonly int m_hashTHit = Animator.StringToHash("tHit");
	private readonly int m_hashTAttack = Animator.StringToHash("tAttack");

	private Animator m_animator;
    private MonsterMove m_monsterMove;
	private MonsterInfo m_monsterInfo;

    [SerializeField]
	private float m_appearTime;

	private bool m_bIsAir;
	private bool m_bLive;

	private MonsterHpBar m_monsterHpBar;
	
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
        m_monsterMove = GetComponent<MonsterMove>();

		#region value bool
		m_bIsAir = false;
		#endregion
		m_monsterHpBar = GetComponentInChildren<MonsterHpBar>();
		m_monsterHpBar.transform.gameObject.SetActive(false);
		m_monsterInfo = GetComponent<MonsterInfo>();

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
				break;
			case ENEMY_STATE.MOVE:
				//WorkCheckHit = new Work(CheckHit(), true);
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
				break;
			case ENEMY_STATE.MOVE:
				if (WorkMove != null) WorkMove.KillCoroutine();
				//if (WorkCheckHit != null) WorkCheckHit.KillCoroutine();
				break;
			case ENEMY_STATE.DIE:
				break;
			case ENEMY_STATE.HIT:
				if (WorkHit != null) WorkHit.KillCoroutine();
				break;
		}
	}

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
			//CheckHit();
            if (time < 0)
            {
                nowState = ENEMY_STATE.MOVE;
            }
			yield return null;
		}
	}

	IEnumerator Attack()
	{
		while(true)
		{

			yield return null;
		}
	}

    IEnumerator Move()
    {
		float time = 3.0f;
		
		while (true)
        {
			Debug_UI.Inst.SetDebugText("Cha_anim", m_animFunction.GetCurrntAnimClipName().ToString());
			Debug_UI.Inst.SetDebugText("isMove", m_monsterMove.isMove.ToString());
			Debug_UI.Inst.SetDebugText("isAir", m_bIsAir.ToString());
			CheckHit();
			CheckDie();
			if (!m_bIsAir)
			{
				m_monsterMove.isMove = true;
				m_animator.SetFloat(m_hashFSpeed, m_fSpeed);
				m_monsterHpBar.SetHpBarDirection(this.transform.localScale.x);
			}
			//time -= Time.deltaTime;
			//if(time <0)
			//{
			//	//m_monsterMove.Move(m_fSpeed);
			//	m_monsterMove.isMove = true;
			//	m_animator.SetFloat(m_hashFSpeed, m_fSpeed);
			//	time = 3.0f;
			//}
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

			CheckDie();
			m_monsterHpBar.SetHPBar(m_monsterInfo);
			m_monsterHpBar.SetHpBarDirection(this.transform.localScale.x);

			time -= Time.deltaTime;
			if(time<0)
			{
				CheckHit();
				nowState = ENEMY_STATE.MOVE;
				//attack 가능?
				//else
				//이동
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




	#region function
	protected void InitAniamation()
	{
		m_animator = this.GetComponentInChildren<Animator>();
		m_animator.SetBool(m_hashBLive, true);
		m_animFunction = transform.GetComponentInChildren<AnimFuntion>();
	}

	protected void CheckCanAttack()
	{
		////test
		//if (StageManager.Inst.playerTransform.position.x - this.transform.position.x > -3 &&
		//    StageManager.Inst.playerTransform.position.x - this.transform.position.x < 3)
		//{
		//    nowState = ENEMY_STATE.NONE;
		//}
	}

	AnimFuntion m_animFunction;
	private void CheckHit()
	{
		if (m_animFunction.GetCurrntAnimClipName() == "hit")
		{
			nowState = ENEMY_STATE.HIT;
		}
	}

	private void CheckDie()
	{
		if(m_monsterInfo.IsCharacterDie())
		{
			nowState = ENEMY_STATE.DIE;
		}
	}
	#endregion

	#region collider
	private void OnTriggerEnter2D(Collider2D collision)
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
