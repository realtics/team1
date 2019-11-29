﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_26
 * 팀              : 1팀
 * 스크립트 용도   : 플레이어 회피관련 함수
 */
public class PlayerEvasion : MonoBehaviour
{
    [SerializeField] private float m_speed = 800.0f;
    [SerializeField] private float m_speedAir = 700.0f;
    [SerializeField] private int m_count = 3;
    private bool m_bCoroutinePlay = false;
    private bool m_bEvasion = false;
    private PlayerState m_playerState = null;
    private AnimFuntion m_animFuntion = null;
    private Rigidbody2D m_rigidbody2D = null;
    private PlayerCrowdControlManager m_crowdControlManager = null;

    private void Awake()
    {
        m_playerState = GetComponent<PlayerState>();
        m_animFuntion = this.transform.Find("PlayerSpineSprite").GetComponent<AnimFuntion>();
        m_rigidbody2D = this.GetComponent<Rigidbody2D>();
        m_crowdControlManager = this.GetComponent<PlayerCrowdControlManager>();
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
        m_crowdControlManager.ImpenetrableOn();
        yield return new WaitForSeconds(0.1f);

        if (!m_playerState.IsPlayerGround())
        {
            m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            AddMeve(m_speedAir);
        }
        else AddMeve(m_speed);

        while (true)
        {
            if (!m_animFuntion.IsTag("Evasion"))
            {
                break;
            }

            yield return 0;
        }

        m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        m_rigidbody2D.velocity = new Vector2(0.0f, 0.1f);
        m_bEvasion = false;
        m_crowdControlManager.ImpenetrableOff();

        if (m_playerState.IsPlayerGround())
            m_playerState.PlayerStateReset();
        else m_playerState.PlayerStateDoubleJump();
    }

    private void AddMeve(float _speed)
    {
        m_rigidbody2D.velocity = new Vector2(0.0f, 0.0f);
        if (m_playerState.IsPlayerLookRight())
            m_rigidbody2D.AddForce(new Vector2(_speed, 0.0f));
        else m_rigidbody2D.AddForce(new Vector2(-_speed, 0.0f));

    }
}
