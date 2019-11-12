using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_18
 * 팀              : 1팀
 * 스크립트 용도   : 플레이어의 발이 땅과 겹쳐있는지 아닌지에 관한 함수, 점프와 떨어지는 것을 판정
 */
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerFootCollision : MonoBehaviour
{
    private PlayerState m_playerState = null;
    private Animator    m_animator = null;
    private Rigidbody2D m_rigidbody2D = null;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = this.GetComponent<Transform>().parent.transform.Find("PlayerSpineSprite").GetComponent<Animator>();
        m_playerState = this.GetComponent<Transform>().parent.GetComponent<PlayerState>();
        m_rigidbody2D = this.GetComponent<Transform>().parent.GetComponent<Rigidbody2D>();

        this.GetComponent<BoxCollider2D>().offset = new Vector2(-0.01191986f, 0.09466985f);
        this.GetComponent<BoxCollider2D>().size = new Vector2(0.9761602f, 0.4479895f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            m_animator.SetTrigger("TriggerLend");
            m_animator.ResetTrigger("TriggerFall");
            m_playerState.PlayerStateReset();
            m_rigidbody2D.velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            if (m_playerState.IsPlayerGround())
            {
                m_animator.SetTrigger("TriggerFall");
            }
            m_animator.ResetTrigger("TriggerLend");
            m_playerState.PlayerStateJump();
        }
    }
}
