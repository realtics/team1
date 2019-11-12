using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttack : MonoBehaviour
{
    private PlayerState m_playerState = null;
    private Animator    m_animator = null;
    // Start is called before the first frame update
    void Start()
    {
        m_playerState   = this.GetComponent<PlayerState>();
        m_animator = this.transform.Find("PlayerSpineSprite").GetComponent<Animator>();
    }

    public void NormalAttack()
    {
        m_animator.SetTrigger("TriggerNormalAttack");
    }
}