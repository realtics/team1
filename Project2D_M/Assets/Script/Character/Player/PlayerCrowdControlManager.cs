using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_18
 * 팀              : 1팀
 * 스크립트 용도   : 플레이어의 상태이상 제어 스크립트
 */
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerState))]
public class PlayerCrowdControlManager : CrowdControlManager
{
	private PlayerInput m_playerInput = null;
	private Rigidbody2D m_rigidbody2D = null;
	private PlayerState m_playerState = null;
	private PlayerAudioFunction m_audioFunction = null;
	private void Awake()
	{
		m_characterMove = this.GetComponent<CharacterMove>();
		m_characterJump = this.GetComponent<CharacterJump>();
		m_playerInput = this.GetComponent<PlayerInput>();
		m_receiveDamage = this.GetComponent<ReceiveDamage>();
		m_rigidbody2D = this.GetComponent<Rigidbody2D>();
		m_playerState = this.GetComponent<PlayerState>();
		m_audioFunction = this.GetComponent<PlayerAudioFunction>();
	}

	public override void Stiffen(float _second)
	{
		if (m_bStiffen == false)
			StartCoroutine(nameof(StiffenCoroutine), _second);
		else
		{
			StopCoroutine(nameof(StiffenCoroutine));
			StartCoroutine(nameof(StiffenCoroutine), _second);
		}
	}

	IEnumerator StiffenCoroutine(float _second)
	{
		if (m_playerInput.bScriptEnable == true)
		{
			m_playerInput.bScriptEnable = false;
			yield return new WaitForSeconds(_second);
			m_playerInput.bScriptEnable = true;
		}
	}

	public void OnAirStop()
	{
		if (!m_playerState.IsPlayerEvasion())
			m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
	}

	public void OffAirStop()
	{
		if (!m_playerState.IsPlayerEvasion())
		{
			m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
			m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, m_rigidbody2D.velocity.y + 0.01f);
		}
	}

	/// <summary>
	/// 승리시
	/// </summary>
	public void PlayerStateClear()
	{
		m_audioFunction.VoicePlay("Victory",false);
		ImpenetrableOn();
	}


	/// <summary>
	/// 패배시
	/// </summary>
	public void PlayerStateFail()
	{
		m_audioFunction.VoicePlay("Death", false);
		ImpenetrableOn();
	}
}
