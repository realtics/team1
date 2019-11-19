using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUiInput : MonoBehaviour
{
    private PlayerState m_playerState = null;
    private CharacterMove m_characterMove = null;
    private CharacterJump m_characterJump = null;
    private PlayerNormalAttack m_playerNormalAttack = null;
    private Animator m_animator = null;

    private PlayerInfo temp;

    public JoyStick joyStick;

    [SerializeField]
    private float m_fMoveSpeed = 10.0f;
    [SerializeField]
    private float m_fJumpforce = 25.0f;

    void Start()
    {
        m_playerState = this.GetComponent<PlayerState>();
        m_characterMove = this.GetComponent<CharacterMove>();
        m_characterJump = this.GetComponent<CharacterJump>();
        m_animator = this.transform.Find("PlayerSpineSprite").GetComponent<Animator>();
        m_playerNormalAttack = this.GetComponent<PlayerNormalAttack>();

        temp = this.GetComponent<PlayerInfo>();
    }

    private void FixedUpdate()
    {
        MoveInput();
        //JumpInput();
        //AttackInput();
    }

    private void OnDisable()
    {
        m_animator.SetBool("bMove", false);
    }

    private void MoveInput()
    {
        if (m_playerState.IsPlayerAttack())
        {
            return;
        }

        Move(m_fMoveSpeed);
    }

    private void Move(float _speed)
    {
        if (joyStick.GetHorizontal() < 0)
        {
            Debug.Log(joyStick.GetHorizontal());
            m_animator.SetBool("bMove", true);
            m_characterMove.MoveLeft(_speed);
        }
        else if (joyStick.GetHorizontal() > 0)
        {
            Debug.Log(joyStick.GetHorizontal());
            m_animator.SetBool("bMove", true);
            m_characterMove.MoveRight(_speed);
        }
        else
        {
            m_animator.SetBool("bMove", false);
            m_characterMove.MoveStop();
        }
    }

    private void JumpInput()
    {
        if (Input.GetButtonDown("Jump") && !m_playerState.IsPlayerAttack())
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

    private void AttackInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            m_playerState.PlayerStateAttack();
            m_playerNormalAttack.NormalAttack();
        }
    }
}