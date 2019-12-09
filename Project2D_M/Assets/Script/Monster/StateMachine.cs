using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine : MonsterInfo
{
	private bool m_bRunning;
	private bool m_bPaused;

	private IEnumerator m_coroutine;

	public enum ENEMY_STATE
	{
		APPEAR,
		IDLE,
		ATTACK,
		MOVE,
		DIE,
		HIT
	}

	private ENEMY_STATE eState;

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

	WaitForSeconds Delay500 = new WaitForSeconds(0.5f);
	WaitForSeconds Delay250 = new WaitForSeconds(0.25f);

	protected virtual void EnterState(ENEMY_STATE _state)
	{
	}

	protected virtual void ExitState(ENEMY_STATE _state)
	{
	}

	protected IEnumerator DoWork()
	{
		yield return null;

		while (m_bRunning)
		{
			if(m_bPaused)
			{
				yield return null;
			}
			else
			{
				if(m_coroutine.MoveNext())
				{
					yield return m_coroutine.Current;
				}
				else
				{
					m_bRunning = false;
				}
			}
		}
	}

	public void start(IEnumerator coroutine)
	{
		m_coroutine = coroutine;
		m_bRunning = true;
		StartCoroutine(DoWork());
	}

	public void KillThis()
	{
		m_bRunning = false;
		m_bPaused = false;
	}
}