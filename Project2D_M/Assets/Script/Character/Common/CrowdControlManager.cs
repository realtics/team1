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
    protected ReceiveDamage m_receiveDamage = null;

    protected bool m_bStiffen = false;
    protected bool m_bImpenetrable = false;
	public bool superArmor { get; protected set; } = false;

    private void Awake()
    {
        m_characterMove = this.GetComponent<CharacterMove>();
        m_characterJump = this.GetComponent<CharacterJump>();
        m_receiveDamage = this.GetComponent<ReceiveDamage>();
    }

    /// <summary>
    /// 경직
    /// </summary>
    /// <param name="_second"></param>
    public virtual void Stiffen(float _second)
    {
        if (m_bStiffen == false)
            StartCoroutine(nameof(StiffenCoroutine), _second);
        else
        {
            StopCoroutine(nameof(StiffenCoroutine));
            StartCoroutine(nameof(StiffenCoroutine), _second);
        }
    }

    IEnumerator StiffenCoroutine(float _second)
    {
        m_bStiffen = true;

        if (m_characterMove != null)
            m_characterMove.enabled = false;

        if (m_characterJump != null)
            m_characterJump.enabled = false;

        yield return new WaitForSeconds(_second);

        if (m_characterMove != null)
            m_characterMove.enabled = true;

        if (m_characterJump != null)
            m_characterJump.enabled = true;

        m_bStiffen = false;
    }

    /// <summary>
    /// 무적(시간)
    /// </summary>
    public virtual void Impenetrable(float _second)
    {
        if (m_bImpenetrable == false)
            StartCoroutine(nameof(ImpenetrableCoroutine), _second);
        else
        {
            StopCoroutine(nameof(ImpenetrableCoroutine));
            StartCoroutine(nameof(ImpenetrableCoroutine), _second);
        }
    }

    /// <summary>
    /// 무적 킴
    /// </summary>
    public virtual void ImpenetrableOn()
    {
        m_bImpenetrable = true;
        m_receiveDamage.bScriptEnable = false;
    }

    /// <summary>
    /// 무적 끄기
    /// </summary>
    public virtual void ImpenetrableOff()
    {
        m_receiveDamage.bScriptEnable = true;
        m_bImpenetrable = false;
    }

	/// <summary>
	/// 경직무적 켜기
	/// </summary>
	public virtual void SuperArmorOn()
	{
		superArmor = true;
	}

	/// <summary>
	/// 경직무적 끄기
	/// </summary>
	public virtual void SuperArmorOff()
	{
		superArmor = false;
	}

	IEnumerator ImpenetrableCoroutine(float _second)
    {
        m_bImpenetrable = true;
        m_receiveDamage.bScriptEnable = false;

        yield return new WaitForSeconds(_second);

        m_receiveDamage.bScriptEnable = true;
        m_bImpenetrable = false;
    }
}
