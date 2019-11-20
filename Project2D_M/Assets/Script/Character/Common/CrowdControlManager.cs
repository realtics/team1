using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_20
 * 팀              : 1팀
 * 스크립트 용도   : 상태이상 담당 클래스 부모
 */
public class CrowdControlManager : MonoBehaviour
{
    protected CharacterMove m_characterMove = null;
    protected CharacterJump m_characterJump = null;

    private void Awake()
    {
        m_characterMove = this.GetComponent<CharacterMove>();
        m_characterJump = this.GetComponent<CharacterJump>();
    }

    public virtual void Stiffen(float _second)
    {
        StartCoroutine(nameof(StiffenCoroutine), _second);
    }

    IEnumerator StiffenCoroutine(float _second)
    {
        if (m_characterMove != null)
            m_characterMove.enabled = false;

        if (m_characterJump != null)
            m_characterJump.enabled = false;

        yield return new WaitForSeconds(_second);

        if (m_characterMove != null)
            m_characterMove.enabled = true;

        if (m_characterJump != null)
            m_characterJump.enabled = true;
    }
}
