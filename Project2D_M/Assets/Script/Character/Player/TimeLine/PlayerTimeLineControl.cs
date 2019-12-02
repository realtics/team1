using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[RequireComponent(typeof(SignalReceiver))]
public class PlayerTimeLineControl : MonoBehaviour
{
	private PlayerInput m_playerInput;
	private PlayerCrowdControlManager m_controlManager;
	private PlayerInfo m_playerInfo;
	private CharacterMove m_characterMove;
	private PlayerAnimFuntion m_animFuntion;
	private bool m_bMove;
	private void Awake()
	{
		m_playerInput = GetComponent<PlayerInput>();
		m_controlManager = GetComponent<PlayerCrowdControlManager>();
		m_playerInfo = GetComponent<PlayerInfo>();
		m_characterMove = GetComponent<CharacterMove>();
		m_animFuntion = GetComponentInChildren<PlayerAnimFuntion>();
	}

	public void MoveStart()
	{
		StartCoroutine(nameof(MoveCoroutine));
	}

	public void MoveStop()
	{
		m_bMove = false;
	}

	public void InputFreeze()
	{
		m_playerInput.bScriptEnable = false;
	}

	public void InputUnFreeze()
	{
		m_playerInput.bScriptEnable = true;
	}

	IEnumerator MoveCoroutine()
	{
		m_bMove = true;
		m_controlManager.ImpenetrableOn();
		m_animFuntion.SetBool(m_animFuntion.hashBMove, true);

		while (true)
		{
			if (this.transform.localScale.x < 0)
			{
				m_characterMove.MoveLeft(m_playerInfo.fMoveSpeed);
			}
			else
				m_characterMove.MoveRight(m_playerInfo.fMoveSpeed);


			yield return null;

			if (!m_bMove)
			{
				m_controlManager.ImpenetrableOff();
				m_animFuntion.SetBool(m_animFuntion.hashBMove, false);
				m_characterMove.MoveStop();
				yield break;
			}
		}
	}
}
