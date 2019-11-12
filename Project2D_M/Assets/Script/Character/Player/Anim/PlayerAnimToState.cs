using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        m_animator.ResetTrigger("TriggerNormalAttack");
        m_playerState.PlayerStateReset();
    }
}
