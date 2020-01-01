using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterStateMachine : MonsterInfo
{



	public enum ENEMY_STATE
	{
		APPEAR,
		IDLE,
		ATTACK,
		MOVE,
		DIE,
		HIT,
		STUN,
		NONE
	}

	private ENEMY_STATE eState = ENEMY_STATE.NONE;

	public ENEMY_STATE nowState
	{
		get
		{
			return eState;
		}
		set
		{
			ExitState(eState);
			eState = value;
			EnterState(eState);
		}
	}

	protected virtual void EnterState(ENEMY_STATE _state)
	{
	}

	protected virtual void ExitState(ENEMY_STATE _state)
	{
	}

	public MonsterStateMachine GetMachine()
	{
		return this;
	}

	
}

public class Work
{
	private bool m_bRunning;
	private bool m_bPaused;

	private IEnumerator m_coroutine;
	protected IEnumerator DoWork()
	{
		yield return null;



		while (m_bRunning)
		{
			if (m_bPaused)
			{
				yield return null;
			}
			else
			{
				if (m_coroutine.MoveNext())
				{
					yield return m_coroutine.Current;
				}
				else
				{
					m_bRunning = false;
					break;
				}
			}
			
		}
	}

	public void ActionStart(/*IEnumerator coroutine*/)
	{
		//m_coroutine = coroutine;
		m_bRunning = true;
		StageManager.Inst.StartCoroutine(DoWork());
	}

	public void KillCoroutine()
	{
		m_bRunning = false;
		m_bPaused = false;
	}
	public Work(IEnumerator coroutine, bool shouldStart)
	{
		m_coroutine = coroutine;

		if (shouldStart)
			ActionStart();
	}

}
