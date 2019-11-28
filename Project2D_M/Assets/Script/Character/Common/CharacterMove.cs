using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_18
 * 팀              : 1팀
 * 스크립트 용도   : 캐릭터의 좌, 우 움직임
 */
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMove : ScriptEnable
{
    private Rigidbody2D m_characterRigidbody = null;
    private bool m_bFlipX = false;

    private void Start()
    {
        m_characterRigidbody = this.GetComponent<Rigidbody2D>();
    }
    public void MoveLeft(float _speed)
    {
        if (!bScriptEnable)
            return;
        if (!m_bFlipX)
        {
            m_bFlipX = true;
            this.transform.localScale += new Vector3(this.transform.localScale.x * -2, 0, 0);
        }
        m_characterRigidbody.velocity = new Vector2(-_speed, m_characterRigidbody.velocity.y);
    }

    public void MoveRight(float _speed)
    {
        if (!bScriptEnable)
            return;
        if (m_bFlipX)
        {
            m_bFlipX = false;
            this.transform.localScale += new Vector3(this.transform.localScale.x * -2, 0, 0);
        }
        m_characterRigidbody.velocity = new Vector2(_speed, m_characterRigidbody.velocity.y);
    }

    public void MoveStop()
    {
        if (!bScriptEnable)
            return;
        m_characterRigidbody.velocity = new Vector2(0, m_characterRigidbody.velocity.y);
    }
}
