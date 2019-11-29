using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_18
 * 팀              : 1팀
 * 스크립트 용도   : 플레이어의 애니메이션 이벤트, 플레이어의 상태, 상태이상과 연결
 */
public class PlayerAnimToState : MonoBehaviour
{
    private PlayerState m_playerState = null;
    private PlayerCrowdControlManager m_playerCrowdControlManager = null;
    private Animator m_animator = null;

    // Start is called before the first frame update
    void Start()
    {
        m_playerCrowdControlManager = this.transform.parent.GetComponent<PlayerCrowdControlManager>();
        m_playerState = this.transform.parent.GetComponent<PlayerState>();
        m_animator = this.GetComponent<Animator>();
    }

    public void Stiffen(float _time)
    {
        m_playerCrowdControlManager.Stiffen(_time);
    }

    public void PlayerStateReset()
    {
        m_animator.ResetTrigger("tDownsmash");
        m_animator.ResetTrigger("tUpper");
        m_animator.ResetTrigger("tNormalAttack");
        m_playerState.PlayerStateReset();
    }
}
