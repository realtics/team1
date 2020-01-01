using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_13
 * 팀              : 1팀
 * 스크립트 용도   : 애니메이터의 자주 쓸것같은 함수 모음
 */
public class AnimFuntion : MonoBehaviour
{
    private Animator m_animator = null;

    private void Awake()
    {
        m_animator = this.GetComponent<Animator>();
    }

    /// <summary>
    /// _animName의 애니메이션이 진행중인지
    /// </summary>
    /// <param name="_animName"></param>
    /// <returns></returns>
    public bool IsAnimationPlayingName(string _animName)
    {
        return (m_animator.GetCurrentAnimatorStateInfo(0).IsName(_animName) &&
            m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.99f);
    }

    public int tagGetHash()
    {
        return m_animator.GetCurrentAnimatorStateInfo(0).tagHash;
    }

    public int tagGetHashst(string _tagname)
    {
        return Animator.StringToHash(_tagname);
    }

	public void PlayAnim(string _animName)
	{
		m_animator.Play(_animName);
	}

    /// <summary>
    /// 현재 진행중인 애니메이션 클립의 이름
    /// </summary>
    /// <returns></returns>
    public string GetCurrntAnimClipName()
    {
		return m_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
    }

    /// <summary>
    /// 현재 진행중인 애니메이션의 진행 초
    /// </summary>
    /// <returns></returns>
    public float GetCurrntClipTime()
    {
        return m_animator.GetCurrentAnimatorStateInfo(0).length * m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public bool IsTag(string _animTag)
    {
        return m_animator.GetCurrentAnimatorStateInfo(0).IsTag(_animTag);
    }

	/// <summary>
	/// 현재 진행중인 애니메이션 스테이트의 이름과 비교
	/// </summary>
	/// <param name="_animName"></param>
	/// <returns></returns>
    public bool IsName(string _animName)
    {
        return m_animator.GetCurrentAnimatorStateInfo(0).IsName(_animName);
    }

    public void SetTrigger(int _triggerName)
    {
        m_animator.SetTrigger(_triggerName);
    }

    public void ResetTrigger(int _triggerName)
    {
        m_animator.ResetTrigger(_triggerName);
    }

    public void SetBool(int _boolName, bool _bool)
    {
        m_animator.SetBool(_boolName, _bool);
    }

    public bool GetBool(int _boolName)
    {
        return m_animator.GetBool(_boolName);
    }

    public IEnumerator AnimTiming(float _time)
    {
        yield return 0;

        while (true)
        {
            if (GetCurrntClipTime() >= _time)
                break;

            yield return 0;
        }
    }
}
