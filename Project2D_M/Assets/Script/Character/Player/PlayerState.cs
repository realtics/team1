using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_18
 * 팀              : 1팀
 * 스크립트 용도   : 플레이어의 현재 상태 참조, 저장
 */
public class PlayerState : MonoBehaviour
{
	//캐릭터의 현재 공간
	private enum PLAYER_STATE_POSITION
    {
        PLAYER_POSITION_AIR,
        PLAYER_POSITION_GROUND
    }

	//캐릭터의 현재 점프의 상태
	private enum PLAYER_STATE_JUMP
    {
        PLAYER_STATE_NONEJUMP,
        PLAYER_STATE_JUMP,
        PLAYER_STATE_DOUBLEJUMP,
    }

    //캐릭터의 현재 액션
    private enum PLAYER_STATE_ACTION
    {
        PLAYER_STATE_STAND, //대기상태(서 있거나 이동할 때)
        PLAYER_STATE_ATTACK,
		PLAYER_STATE_SP_ATTACK,
        PlAYER_STATE_EVASION,
        PLAYER_STATE_JUMP,
    }

	public bool bSkipAction = true;

    [Header("Player State")]
    [SerializeField] private PLAYER_STATE_POSITION m_positionState = PLAYER_STATE_POSITION.PLAYER_POSITION_GROUND;
    [SerializeField] private PLAYER_STATE_JUMP m_jumpState = PLAYER_STATE_JUMP.PLAYER_STATE_NONEJUMP;
    [SerializeField] private PLAYER_STATE_ACTION m_actionState  = PLAYER_STATE_ACTION.PLAYER_STATE_STAND;

    /// <summary>
    ///플레이어 상태 초기화
    /// </summary>
    public void PlayerStateReset()
    {
        m_positionState = PLAYER_STATE_POSITION.PLAYER_POSITION_GROUND;
        m_jumpState = PLAYER_STATE_JUMP.PLAYER_STATE_NONEJUMP;
        m_actionState = PLAYER_STATE_ACTION.PLAYER_STATE_STAND;
    }

    /// <summary>
    /// 플레이어가 떨어짐(공격 불가능)
    /// </summary>
    public void PlayerStateFalling()
    {
        m_positionState = PLAYER_STATE_POSITION.PLAYER_POSITION_AIR;
        m_jumpState = PLAYER_STATE_JUMP.PLAYER_STATE_JUMP;
        m_actionState = PLAYER_STATE_ACTION.PLAYER_STATE_JUMP;
    }

    /// <summary>
    /// 플레이어 상태를 공격으로
    /// </summary>
    public void PlayerStateAttack()
    {
        m_actionState = PLAYER_STATE_ACTION.PLAYER_STATE_ATTACK;
    }

	/// <summary>
	/// 플레이어 상태를 특수공격으로
	/// </summary>
	public void PlayerStateSPAttack()
	{
		m_actionState = PLAYER_STATE_ACTION.PLAYER_STATE_SP_ATTACK;
	}

	/// <summary>
	/// 플레이어 상태를 회비로
	/// </summary>
	public void PlayerStateEvasion()
    {
        m_actionState = PLAYER_STATE_ACTION.PlAYER_STATE_EVASION;
    }

    /// <summary>
    /// 플레이어가 한번 점프한 상태
    /// </summary>
    public void PlayerStateJump()
    {
        m_positionState = PLAYER_STATE_POSITION.PLAYER_POSITION_AIR;
        m_jumpState = PLAYER_STATE_JUMP.PLAYER_STATE_JUMP;
		if(m_actionState != PLAYER_STATE_ACTION.PLAYER_STATE_ATTACK)
        m_actionState = PLAYER_STATE_ACTION.PLAYER_STATE_JUMP;
    }

    /// <summary>
    /// 플레이어가 두번 점프한 상태
    /// </summary>
    public void PlayerStateDoubleJump()
    {
        m_positionState = PLAYER_STATE_POSITION.PLAYER_POSITION_AIR;
        m_jumpState = PLAYER_STATE_JUMP.PLAYER_STATE_DOUBLEJUMP;
        m_actionState = PLAYER_STATE_ACTION.PLAYER_STATE_JUMP;
    }
    
    /// <summary>
    /// 플레이어가 이동 조작이 가능한가?
    /// </summary>
    public bool IsPlayerMove()
    {
        if (m_actionState == PLAYER_STATE_ACTION.PLAYER_STATE_ATTACK ||
			m_actionState == PLAYER_STATE_ACTION.PLAYER_STATE_SP_ATTACK || 
			m_actionState == PLAYER_STATE_ACTION.PlAYER_STATE_EVASION)
            return false;

        return true;
    }

	/// <summary>
	/// 플레이어가 공격상태인가?
	/// </summary>
	public bool IsPlayerAttack()
	{
		return (m_actionState == PLAYER_STATE_ACTION.PLAYER_STATE_ATTACK ||
			m_actionState == PLAYER_STATE_ACTION.PLAYER_STATE_SP_ATTACK);
	}

	/// <summary>
	/// 플레이어가 특수공격상태인가?
	/// </summary>
	public bool IsPlayerSPAttack()
	{
		return m_actionState == PLAYER_STATE_ACTION.PLAYER_STATE_SP_ATTACK;
	}


	/// <summary>
	/// 플레이어가 회피상태인가?
	/// </summary>
	public bool IsPlayerEvasion()
    {
        if (m_actionState == PLAYER_STATE_ACTION.PlAYER_STATE_EVASION)
            return true;

        return false;
    }

    /// <summary>
    /// 플레이어가 오른쪽을 보는가?
    /// </summary>
    public bool IsPlayerLookRight()
    {
        if (this.transform.localScale.x < 0)
            return false;
        return true;
    }

    /// <summary>
    /// 플레이어가 지상인가?
    /// </summary>
    public bool IsPlayerGround()
    {
        if (m_positionState == PLAYER_STATE_POSITION.PLAYER_POSITION_GROUND)
            return true;

        return false;
    }

    /// <summary>
    /// 플레이어가 두번째 점프가 가능한가?
    /// </summary>
    public bool IsPlayerDoubleJump()
    {
        if (m_positionState != PLAYER_STATE_POSITION.PLAYER_POSITION_AIR)
            return false;

        if (m_jumpState == PLAYER_STATE_JUMP.PLAYER_STATE_DOUBLEJUMP)
            return false;

        return true;
    }

    /// <summary>
    /// 플레이어가 첫번째 점프가 가능한가?
    /// </summary>
    public bool IsPlayerJump()
    {
        if (m_positionState == PLAYER_STATE_POSITION.PLAYER_POSITION_AIR)
            return false;

        if (m_jumpState != PLAYER_STATE_JUMP.PLAYER_STATE_NONEJUMP)
            return false;

        return true;
    }
}