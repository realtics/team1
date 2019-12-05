using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine : MonsterInfo
{
	public enum ENEMY_STATE
	{
		APPEAR,
		IDLE,
		ATTACK,
		MOVE,
		DIE,
		HIT
	}

	public ENEMY_STATE state;

	WaitForSeconds Delay500 = new WaitForSeconds(0.5f);
	WaitForSeconds Delay250 = new WaitForSeconds(0.25f);

	protected void Start()
	{
		state = ENEMY_STATE.APPEAR;
	}

	protected virtual void InitMonster() { }


}
