using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_25
 * 팀              : 1팀
 * 스크립트 용도   : 데미지폰트의 데미지에 따른 출력과 사라지는 연출
 */
public class DamageFont : MonoBehaviour
{
	private enum FONT_MARK
	{
		CRITICAL_MARK = 5,
		MISS_MARK
	}

    public struct DamageFontOption
    {
        public Sprite criticalMark;
		public Sprite missMark;
        public Sprite[] criticalDamageFont;
        public Sprite[] normalDamageFont;

        public Vector2 fontSize;
        public float fontSpace;
        public float upSpeed;
        public float clearSpeed;
    }

    private SpriteRenderer[] m_spriteRenderer;
    private DamageFontOption m_option;

    public void DamageFontInit(DamageFontOption _damageFontOption)
    {
        m_option = _damageFontOption;
        this.transform.localScale = new Vector3(m_option.fontSize.x, m_option.fontSize.y, 1);
    }

    public void SetDamage(int _damage, bool _bCritical)
    {

		if (m_spriteRenderer == null)
		{
			SpriteRenderersInit();
		}

		if (_damage == 0)
		{
			m_spriteRenderer[(int)FONT_MARK.MISS_MARK].enabled = true;
			m_spriteRenderer[(int)FONT_MARK.MISS_MARK].sprite = m_option.missMark;
			StartCoroutine(nameof(FontMove));
			return;
		}

        if (_damage > 99999)
            _damage = 99999;

        string damageStr = _damage.ToString();
        
        Sprite[] currntSprites;

        if (_bCritical)
        {
            currntSprites = m_option.criticalDamageFont;
            m_spriteRenderer[(int)FONT_MARK.CRITICAL_MARK].enabled = true;
            m_spriteRenderer[(int)FONT_MARK.CRITICAL_MARK].sprite = m_option.criticalMark;
        }
        else currntSprites = m_option.normalDamageFont;

        for (int i = 0; i < damageStr.Length; ++i)
        {
            m_spriteRenderer[i].enabled = true;
            m_spriteRenderer[i].sprite  = currntSprites[(int)(damageStr[i] - '0')];
        }

        StartCoroutine(nameof(FontMove));
    }

    private void OnDisable()
    {
        if (m_spriteRenderer != null)
        {
            for (int i = 0; i < m_spriteRenderer.Length; ++i)
            {
                Color color = m_spriteRenderer[i].color;
                color.a = 1.0f;
                m_spriteRenderer[i].color = color;
                m_spriteRenderer[i].enabled = false;
            }
        }

        if (m_option.criticalDamageFont != null)
			m_option.criticalDamageFont.Initialize();

        StopCoroutine(nameof(FontMove));
    }

    IEnumerator FontMove()
    {
        while (true)
        {
            this.transform.position += new Vector3(0, m_option.upSpeed, 0);
            for (int i = 0; i < m_spriteRenderer.Length; ++i)
            {
                if (m_spriteRenderer[i].enabled == true)
                {
                    Color color = m_spriteRenderer[i].color;
                    color.a = color.a - m_option.clearSpeed;
                    m_spriteRenderer[i].color = color;
                    if (color.a <= 0)
                        ObjectPool.Inst.PushToPool(gameObject);
                }
            }
            yield return null;
        }
    }

    private void SpriteRenderersInit()
    {
        m_spriteRenderer = this.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < (int)FONT_MARK.CRITICAL_MARK; ++i)
        { 
            m_spriteRenderer[i].transform.position += new Vector3(m_option.fontSpace * i, 0, 0);
        }
    }
}