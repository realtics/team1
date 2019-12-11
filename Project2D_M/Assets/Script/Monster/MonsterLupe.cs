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

	void Start()
    {
		InitAniamation();
		nowState = ENEMY_STATE.APPEAR;
		m_AppearTime = 3f;
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
				break;
			case ENEMY_STATE.DIE:
				break;
			case ENEMY_STATE.HIT:
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
				break;
			case ENEMY_STATE.DIE:
				break;
			case ENEMY_STATE.HIT:
				break;
		}
	}

	IEnumerator Appear()
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
            if (time < 0)
            {
                nowState = ENEMY_STATE.MOVE;
            }
			yield return new WaitForSeconds(0.3f);
		}
	}

    IEnumerator Move()
    {
        while(true)
        {
            m_monsterMove.Move(m_fSpeed);
			if(StageManager.Inst.playerTransform.position.x - this.transform.position.x > -3 &&
				StageManager.Inst.playerTransform.position.x - this.transform.position.x < 3)
			{
				nowState = ENEMY_STATE.NONE;
			}
			yield return new WaitForSeconds(0.5f);
		}
	}

	void InitAniamation()
	{
		m_animator = this.GetComponentInChildren<Animator>();
		m_animator.SetBool(m_hashBLive, true);
	}
}
