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

	//[SerializeField]
	private Animator m_animator;
    //[SerializeField]
    private MonsterMove m_monsterMove;
    [SerializeField]
	private float m_AppearTime;

	private Work WorkAppear;
	private Work WorkIdle;
	private Work WorkMove;
	private Work WorkHit;
	private Work WorkCheckHit;

	void Start()
    {
		InitAniamation();
		nowState = ENEMY_STATE.APPEAR;
		//m_AppearTime;
        m_monsterMove = GetComponent<MonsterMove>();

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
				WorkMove = new Work(Move(), true);
				WorkCheckHit = new Work(CheckHit(), true);
				break;
			case ENEMY_STATE.DIE:
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
				//KillCoroutine();
				break;
			case ENEMY_STATE.MOVE:
				if (WorkMove != null) WorkMove.KillCoroutine();
				if (WorkCheckHit != null) WorkCheckHit.KillCoroutine();
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
			m_AppearTime -= Time.deltaTime;
			if (m_AppearTime < 0)
			{
				m_animator.SetBool(m_hashBAppear, true);
                nowState = ENEMY_STATE.IDLE;
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

    IEnumerator Move()
    {
		float time = 3.0f;
		m_monsterMove.isMove = true;
		m_animator.SetFloat(m_hashFSpeed, m_fSpeed);
		while (true)
        {
			Debug_UI.Inst.SetDebugText("Cha_anim", m_animFunction.GetCurrntAnimClipName().ToString());
			Debug_UI.Inst.SetDebugText("isMove", m_monsterMove.isMove.ToString());
			//CheckHit();
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
		float time = 3f;
		m_monsterMove.isMove = false;
		m_animator.SetFloat(m_hashFSpeed, 0);
		while (true)
		{
			time -= Time.deltaTime;
			if(time<0)
			{
				nowState = ENEMY_STATE.MOVE;
				//attack 가능?
				//else
				//이동
			}
			yield return null;
		}
	}

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
	IEnumerator CheckHit()
	{
		while(true)
		{
			if (m_animFunction.GetCurrntAnimClipName() == "Hit")
				nowState = ENEMY_STATE.HIT;

			yield return null;
		}
	}

}
