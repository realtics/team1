using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_18
 * 팀              : 1팀
 * 스크립트 용도   : 캐릭터의 점프 함수
 */

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterJump : ScriptEnable
{
    private Rigidbody2D m_characterRigidbody = null;
    private void Start()
    {
        m_characterRigidbody = this.GetComponent<Rigidbody2D>();
    }
    public void Jump(float _jumpForce)
    {
        if (!bScriptEnable)
            return;

        m_characterRigidbody.velocity = new Vector2(m_characterRigidbody.velocity.x, 0.0f);
        m_characterRigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
    }
}