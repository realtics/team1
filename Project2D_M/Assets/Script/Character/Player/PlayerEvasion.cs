using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvasion : MonoBehaviour
{
    [SerializeField] private int m_count = 3;
    private bool m_bCoroutinePlay = false;
    private bool m_bEvasion = false;
    private PlayerState m_playerState = null;
    private AnimFuntion m_animFuntion = null;
    private Rigidbody2D m_rigidbody2D = null;
    private CharacterMove m_characterMove = null;
    private PlayerUiInput m_playerUiInput = null;

    private void Awake()
    {
        m_playerState = GetComponent<PlayerState>();
        m_animFuntion = this.transform.Find("PlayerSpineSprite").GetComponent<AnimFuntion>();
        m_rigidbody2D = this.GetComponent<Rigidbody2D>();
        m_characterMove = this.GetComponent<CharacterMove>();
        m_playerUiInput = this.GetComponent<PlayerUiInput>();
    }

    public void Evasion()
    {
        if (m_count > 0)
        {
            m_count--;

            if (!m_bEvasion)
            {
                if (m_playerState.IsPlayerGround())
                    m_animFuntion.SetTrigger("tEvasion");
                else m_animFuntion.SetTrigger("tEvasionAir");

                m_bEvasion = true;
                StartCoroutine(nameof(EvasionCoroutine));

                if (!m_bCoroutinePlay)
                    StartCoroutine(nameof(EvasionRecovery));
            }
        }
    }

    IEnumerator EvasionRecovery()
    {
        m_bCoroutinePlay = true;
        while(true)
        {
            yield return new WaitForSeconds(1f);
            m_count++;

            if (m_count >= 3)
                break;
        }
        m_bCoroutinePlay = false;
    }

    IEnumerator EvasionCoroutine()
    {

        yield return 0;

        //if (!m_animFuntion.IsTag("Evasion"))
        //{
        //    m_bEvasion = false;
        //    yield break;
        //}
        m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        AddMeve(10.0f);
        Debug.Log(m_rigidbody2D.velocity);

        while (true)
        {
            Debug.Log(m_animFuntion.GetCurrntAnimClipName());
            if (!m_animFuntion.IsTag("Evasion"))
            {
                break;
            }

            yield return 0;
        }

        m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        m_rigidbody2D.AddForce(new Vector2(0.1f, 0.1f));
        m_bEvasion = false;
    }

    private void AddMeve(float _speed)
    {
        if (Input.GetAxisRaw("Horizontal") < 0 || m_playerUiInput.joystickState == PlayerUiInput.JOYSTICK_STATE.JOYSTICK_LEFT)
        {
            m_characterMove.MoveLeft(_speed);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0 || m_playerUiInput.joystickState == PlayerUiInput.JOYSTICK_STATE.JOYSTICK_RIGHT)
        {
            m_characterMove.MoveRight(_speed);
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 || m_playerUiInput.joystickState == PlayerUiInput.JOYSTICK_STATE.JOYSTICK_CENTER)
        {

        }

    }
}
