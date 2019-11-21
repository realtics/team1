﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_18
 * 팀              : 1팀
 * 스크립트 용도   : 플레이어의 상태이상 제어 스크립트
 */
[RequireComponent(typeof(PlayerInput))]
public class PlayerCrowdControlManager : CrowdControlManager
{
    private PlayerInput     m_playerInput = null;
    private PlayerUiInput m_playerUiInput = null;
    private void Awake()
    {
        m_characterMove = this.GetComponent<CharacterMove>();
        m_characterJump = this.GetComponent<CharacterJump>();
        m_playerInput = this.GetComponent<PlayerInput>();
        m_playerUiInput = this.GetComponent<PlayerUiInput>();
    }

    public override void Stiffen(float _second)
    {
        StartCoroutine(nameof(StiffenCoroutine), _second);
    }

    IEnumerator StiffenCoroutine(float _second)
    {
        if (m_playerInput.enabled == true)
        {
            m_playerInput.enabled = false;
            yield return new WaitForSeconds(_second);
            m_playerInput.enabled = true;
        }
        else if (m_playerUiInput.enabled == true)
        {
            m_playerUiInput.enabled = false;
            yield return new WaitForSeconds(_second);
            m_playerUiInput.enabled = true;
        }
    }
}
