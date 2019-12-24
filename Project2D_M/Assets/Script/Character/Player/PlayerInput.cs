using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_18
 * 팀              : 1팀
 * 스크립트 용도   : 플레이어의 키 입력과 그에 따른 애니메이션 파라미터 조정
 */

[RequireComponent(typeof(CharacterMove))]
[RequireComponent(typeof(CharacterJump))]
[RequireComponent(typeof(PlayerNormalAttack))]
[RequireComponent(typeof(PlayerEvasion))]
public class PlayerInput : ScriptEnable
{
    public enum JOYSTICK_STATE
    {
        JOYSTICK_UP,
        JOYSTICK_DOWN,
        JOYSTICK_LEFT,
        JOYSTICK_RIGHT,
        JOYSTICK_CENTER,
    }

    private PlayerState     m_playerState = null;
    private CharacterMove   m_characterMove = null;
    private CharacterJump   m_characterJump = null;
    private PlayerNormalAttack m_playerNormalAttack = null;
    private PlayerEvasion m_playerEvasion = null;
    private PlayerAnimFuntion m_animFuntion= null;
	private SkillManager m_skillManager = null;
    private PlayerInfo m_playerInfo;
	private AudioFunction m_audioFunction = null;
    private bool m_bUiMove = false;

	[SerializeField] public JOYSTICK_STATE joystickState = JOYSTICK_STATE.JOYSTICK_CENTER;
    [SerializeField] private GameObject m_underUI = null;

    void Start()
    {
        m_playerState    = this.GetComponent<PlayerState>();
        m_characterMove  = this.GetComponent<CharacterMove>();
        m_characterJump  = this.GetComponent<CharacterJump>();
		m_animFuntion = this.GetComponentInChildren<PlayerAnimFuntion>();
		m_skillManager = this.GetComponentInChildren<SkillManager>();
		m_playerNormalAttack = this.GetComponent<PlayerNormalAttack>();
        m_playerEvasion = this.GetComponent<PlayerEvasion>();
        m_playerInfo = this.GetComponent<PlayerInfo>();
		m_audioFunction = this.GetComponent<AudioFunction>();

	}

    private void FixedUpdate()
    {
        if (!bScriptEnable)
            return;

        MoveInput();

        if (Input.GetButtonDown("Jump") && m_playerState.IsPlayerMove())
            JumpInput();

        if (Input.GetButtonDown("Fire1"))
            AttackInput();

        if (Input.GetButtonDown("Fire2"))
            EvasionInput();

		if (Input.GetButtonDown("Fire3"))
		{
			m_skillManager.SkillAction("FlameHaze");
		}

		if (Input.GetKeyDown("w"))
		{
			m_skillManager.SkillAction("FireBallShoot");
		}

	}

    private void MoveInput()
    {
        if (!m_playerState.IsPlayerMove() || m_bUiMove)
        {
            return;
        }

        Move();
    }

    private void Move()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
			m_animFuntion.SetBool(m_animFuntion.hashBMove, true);
            m_characterMove.MoveLeft(m_playerInfo.fMoveSpeed);
            m_underUI.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
			m_animFuntion.SetBool(m_animFuntion.hashBMove, true);
            m_characterMove.MoveRight(m_playerInfo.fMoveSpeed);
            m_underUI.transform.localScale= new Vector3(1,1,1);
        }
		else if (m_animFuntion.GetBool(m_animFuntion.hashBMove))
		{
			m_animFuntion.SetBool(m_animFuntion.hashBMove, false);
            m_characterMove.MoveStop();
        }
    }

    public void JoyStickMove(JOYSTICK_STATE _joyStickState)
    {
        if (!bScriptEnable)
            return;

        joystickState = _joyStickState;

        if (!m_playerState.IsPlayerMove())
        {
            return;
        }

        m_bUiMove = true;

        if (_joyStickState == JOYSTICK_STATE.JOYSTICK_LEFT)
        {
			m_animFuntion.SetBool(m_animFuntion.hashBMove, true);
            m_characterMove.MoveLeft(m_playerInfo.fMoveSpeed);
            m_underUI.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_joyStickState == JOYSTICK_STATE.JOYSTICK_RIGHT)
        {
			m_animFuntion.SetBool(m_animFuntion.hashBMove, true);
            m_characterMove.MoveRight(m_playerInfo.fMoveSpeed);
            m_underUI.transform.localScale = new Vector3(1, 1, 1);
        }
		else if(m_animFuntion.GetBool(m_animFuntion.hashBMove))
		{
            m_bUiMove = false;
			m_animFuntion.SetBool(m_animFuntion.hashBMove, false);
            m_characterMove.MoveStop();
        }
    }

    public void JumpInput()
    {
        if (!bScriptEnable)
            return;

        if (m_playerState.IsPlayerMove())
        {
            if (m_playerState.IsPlayerDoubleJump())
            {
                m_playerState.PlayerStateDoubleJump();
                Jump();
                return;
            }
            else if (m_playerState.IsPlayerJump())
            {
                m_playerState.PlayerStateJump();
                Jump();

                return;
            }
        }
    }

    private void Jump()
    {
		m_animFuntion.SetTrigger(m_animFuntion.hashBJump);
        m_characterJump.Jump(m_playerInfo.fJumpforce);
		m_audioFunction.AudioPlay("Jump", false);
	}

    public void AttackInput()
    {
        if (!bScriptEnable)
            return;

        m_playerNormalAttack.NormalAttack();
    }

    public void EvasionInput()
    {
        if (!bScriptEnable)
            return;

        m_playerEvasion.Evasion();
    }

	public bool SkillAction(string _skillName)
	{
		if (m_playerState.bSkipAction)
		{
			if(m_skillManager.SkillAction(_skillName))
				return true;
		}

		return false;
	}
}