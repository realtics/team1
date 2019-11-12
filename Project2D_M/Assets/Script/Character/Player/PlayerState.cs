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
    public enum PLAYER_STATE_POSITION
    {
        PLAYER_POSITION_AIR,
        PLAYER_POSITION_GROUND
    }

    //캐릭터의 현재 점프의 상태
    public enum PLAYER_STATE_JUMP
    {
        PLAYER_STATE_NONEJUMP,
        PLAYER_STATE_JUMP,
        PLAYER_STATE_DOUBLEJUMP,
    }

    //캐릭터의 현재 액션
    public enum PLAYER_STATE
    {
        PLAYER_STATE_STAND, //대기상태(서 있거나 이동할 때)
        PLAYER_STATE_ATTACK,
        PLAYER_STATE_JUMP,
    }

    public PLAYER_STATE_POSITION playerStatePosition = PLAYER_STATE_POSITION.PLAYER_POSITION_GROUND;
    public PLAYER_STATE_JUMP playerJumpState = PLAYER_STATE_JUMP.PLAYER_STATE_NONEJUMP;
    public PLAYER_STATE playerState  = PLAYER_STATE.PLAYER_STATE_STAND;

    /// <summary>
    ///플레이어 상태 초기화
    /// </summary>
    public void PlayerStateReset()
    {
        playerStatePosition = PLAYER_STATE_POSITION.PLAYER_POSITION_GROUND;
        playerJumpState = PLAYER_STATE_JUMP.PLAYER_STATE_NONEJUMP;
        playerState = PLAYER_STATE.PLAYER_STATE_STAND;
    }

    /// <summary>
    /// 플레이어가 떨어짐(공격 불가능)
    /// </summary>
    public void PlayerStateFalling()
    {
        playerStatePosition = PLAYER_STATE_POSITION.PLAYER_POSITION_AIR;
        playerJumpState = PLAYER_STATE_JUMP.PLAYER_STATE_JUMP;
        playerState = PLAYER_STATE.PLAYER_STATE_JUMP;
    }

    /// <summary>
    /// 플레이어가 한번 점프한 상태
    /// </summary>
    public void PlayerStateJump()
    {
        playerStatePosition = PLAYER_STATE_POSITION.PLAYER_POSITION_AIR;
        playerJumpState = PLAYER_STATE_JUMP.PLAYER_STATE_JUMP;
        playerState = PLAYER_STATE.PLAYER_STATE_JUMP;
    }

    /// <summary>
    /// 플레이어가 두번 점프한 상태
    /// </summary>
    public void PlayerStateDoubleJump()
    {
        playerStatePosition = PLAYER_STATE_POSITION.PLAYER_POSITION_AIR;
        playerJumpState = PLAYER_STATE_JUMP.PLAYER_STATE_DOUBLEJUMP;
        playerState = PLAYER_STATE.PLAYER_STATE_JUMP;
    }
    
    /// <summary>
    /// 플레이어가 대기상태인가?
    /// </summary>
    public bool IsPlayerAttack()
    {
        if (playerState == PLAYER_STATE.PLAYER_STATE_ATTACK)
            return true;

        return false;
    }

    /// <summary>
    /// 플레이어가 지상인가?
    /// </summary>
    public bool IsPlayerGround()
    {
        if (playerStatePosition == PLAYER_STATE_POSITION.PLAYER_POSITION_GROUND)
            return true;

        return false;
    }

    /// <summary>
    /// 플레이어가 공중 평타공격이 가능한가?
    /// </summary>
    public bool IsPlayerAirAttack()
    {
        if (playerStatePosition != PLAYER_STATE_POSITION.PLAYER_POSITION_AIR)
            return false;

        playerState = PLAYER_STATE.PLAYER_STATE_JUMP;

        return true;
    }

    /// <summary>
    /// 플레이어가 두번째 점프가 가능한가?
    /// </summary>
    public bool IsPlayerDoubleJump()
    {
        if (playerStatePosition != PLAYER_STATE_POSITION.PLAYER_POSITION_AIR)
            return false;

        if (playerJumpState == PLAYER_STATE_JUMP.PLAYER_STATE_DOUBLEJUMP)
            return false;

        return true;
    }

    /// <summary>
    /// 플레이어가 첫번째 점프가 가능한가?
    /// </summary>
    public bool IsPlayerJump()
    {
        if (playerStatePosition == PLAYER_STATE_POSITION.PLAYER_POSITION_AIR)
            return false;

        if (playerJumpState != PLAYER_STATE_JUMP.PLAYER_STATE_NONEJUMP)
            return false;

        return true;
    }
}