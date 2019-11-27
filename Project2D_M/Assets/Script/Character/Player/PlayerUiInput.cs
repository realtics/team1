using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMove))]
[RequireComponent(typeof(CharacterJump))]
[RequireComponent(typeof(PlayerNormalAttack))]
[RequireComponent(typeof(PlayerEvasion))]
public class PlayerUiInput : MonoBehaviour
{
    public enum JOYSTICK_STATE
    {
        JOYSTICK_UP,
        JOYSTICK_DOWN,
        JOYSTICK_LEFT,
        JOYSTICK_RIGHT,
        JOYSTICK_CENTER,
    }

    private PlayerState m_playerState = null;
    private CharacterMove m_characterMove = null;
    private CharacterJump m_characterJump = null;
    private PlayerNormalAttack m_playerNormalAttack = null;
    private PlayerEvasion m_playerEvasion = null;
    private Animator m_animator = null;

    [SerializeField] public JOYSTICK_STATE joystickState;
    [SerializeField] private float m_fMoveSpeed = 10.0f;
    [SerializeField] private float m_fJumpforce = 17.0f;

    private void Awake()
    {
        m_playerState = this.GetComponent<PlayerState>();
        m_characterMove = this.GetComponent<CharacterMove>();
        m_characterJump = this.GetComponent<CharacterJump>();
        m_animator = this.transform.Find("PlayerSpineSprite").GetComponent<Animator>();
        m_playerNormalAttack = this.GetComponent<PlayerNormalAttack>();
        m_playerEvasion = this.GetComponent<PlayerEvasion>();
        joystickState = JOYSTICK_STATE.JOYSTICK_CENTER;
    }

    private void OnDisable()
    {
        joystickState = JOYSTICK_STATE.JOYSTICK_CENTER;
        m_animator.SetBool("bMove", false);
    }

    public void JoyStickMove(JOYSTICK_STATE _joyStickState)
    {
        joystickState = _joyStickState;

        if (!m_playerState.IsPlayerMove())
        {
            return;
        }

        if (_joyStickState == JOYSTICK_STATE.JOYSTICK_LEFT)
        {
            m_animator.SetBool("bMove", true);
            m_characterMove.MoveLeft(m_fMoveSpeed);
        }
        else if (_joyStickState == JOYSTICK_STATE.JOYSTICK_RIGHT)
        {
            m_animator.SetBool("bMove", true);
            m_characterMove.MoveRight(m_fMoveSpeed);
        }
        else
        {
            m_animator.SetBool("bMove", false);
            m_characterMove.MoveStop();
        }
    }

    public void JumpInput()
    {
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
        m_animator.SetTrigger("tJump");
        m_characterJump.Jump(m_fJumpforce);
    }

    public void AttackInput()
    {
        m_playerState.PlayerStateAttack();
        m_playerNormalAttack.NormalAttack();
    }

    public void EvasionInput()
    {
        m_playerState.PlayerStateEvasion();
        m_playerEvasion.Evasion();
    }
}