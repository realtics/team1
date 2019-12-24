using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_08
 * 팀              : 1팀
 * 스크립트 용도   : 플레이어의 발이 땅과 겹쳐있는지 아닌지에 관한 함수, 점프와 떨어지는 것을 판정
 */
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerFootCollision : MonoBehaviour
{
    private PlayerState m_playerState = null;
    private PlayerAnimFuntion m_animFuntion = null;
	private AudioFunction m_audioFunction = null;
    private void Awake()
    {
		m_animFuntion = this.transform.parent.transform.Find("PlayerSpineSprite").GetComponent<PlayerAnimFuntion>();
        m_playerState = this.transform.parent.GetComponent<PlayerState>();
		m_audioFunction = this.transform.parent.GetComponent<AudioFunction>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
			m_animFuntion.SetTrigger(m_animFuntion.hashTLend);
			m_animFuntion.SetBool(m_animFuntion.hashbAir,false);
			m_animFuntion.ResetTrigger(m_animFuntion.hashTFall);
			m_animFuntion.ResetTrigger(m_animFuntion.hashTEvasion);

			m_audioFunction.AudioPlay("Lend",false);
			m_playerState.PlayerStateReset();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            if (m_playerState.IsPlayerGround() && !m_playerState.IsPlayerSPAttack())
            {
				m_animFuntion.SetTrigger(m_animFuntion.hashTFall);
            }
			m_animFuntion.ResetTrigger(m_animFuntion.hashTLend);
			m_animFuntion.SetBool(m_animFuntion.hashbAir, true);

			if (!m_playerState.IsPlayerSPAttack())
				m_playerState.PlayerStateJump();
		}
	}
}