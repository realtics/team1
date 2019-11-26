using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFont : MonoBehaviour
{
    public struct DamageFontOption
    {
        public Sprite criticalMark;
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

    public void SetDamage(int damage, bool _bCritical = false)
    {
        if (damage > 99999)
            damage = 99999;

        string damageStr = damage.ToString();

        if(m_spriteRenderer == null)
        {
            SpriteRenderersInit();
        }
        
        Sprite[] currntSprites;

        if (_bCritical)
        {
            currntSprites = m_option.criticalDamageFont;
            m_spriteRenderer[m_spriteRenderer.Length - 1].enabled = true;
            m_spriteRenderer[m_spriteRenderer.Length - 1].sprite = m_option.criticalMark;
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
                        ObjectPool.Inst.PushToPool(this.name, gameObject);
                }
            }
            yield return null;
        }
    }

    private void SpriteRenderersInit()
    {
        m_spriteRenderer = this.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < m_spriteRenderer.Length-1; ++i)
        { 
            m_spriteRenderer[i].transform.position += new Vector3(m_option.fontSpace * i, 0, 0);
        }
    }
}