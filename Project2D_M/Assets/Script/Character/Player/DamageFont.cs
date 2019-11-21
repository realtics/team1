using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFont : MonoBehaviour
{
    private Sprite[] m_criticalDamageFont = null;
    private SpriteRenderer[] m_spriteRenderer = null;
    private Vector2 m_FontSize;
    private float m_FontSpace;
    private void Start()
    {
       m_spriteRenderer = this.GetComponentsInChildren<SpriteRenderer>();
    }

    public void DamageFontInit(Sprite[] sprites, Vector2 _fontSize, float _fontSpace)
    {
        m_criticalDamageFont = sprites;
        m_FontSpace = _fontSpace;
        m_FontSize = _fontSize;
    }

    private void OnEnable()
    {
        
    }

    public void SetDamage(int damage)
    {
        this.transform.localScale = new Vector3(m_FontSize.x, m_FontSize.y, 1);
        if (damage > 99999)
            damage = 99999;

        string damageStr = damage.ToString();

        for (int i = 0; i < damageStr.Length; ++i)
        {
            m_spriteRenderer[i].enabled = true;
            m_spriteRenderer[i].sprite = m_criticalDamageFont[(int)(damageStr[i] - '0')];
            Vector3 pos = m_spriteRenderer[i].transform.position;
            m_spriteRenderer[i].transform.position += new Vector3(pos.x + m_FontSpace * i, 0, 0);
        }

        StartCoroutine(nameof(FontMove));
    }

    private void OnDisable()
    {
        for (int i = 0; i < m_spriteRenderer.Length; ++i)
        {
            m_spriteRenderer[i].enabled = false;
        }

        StopCoroutine(nameof(FontMove));
    }

    IEnumerator FontMove()
    {
        while(true)
        {
            for (int i = 0; i < m_spriteRenderer.Length; ++i)
            {
                if(m_spriteRenderer[i].enabled == true)
                {
                    m_spriteRenderer[i].transform.position += new Vector3(0,0.01f,0);
                    Color color = m_spriteRenderer[i].color;
                    color.a = color.a - 0.1f;
                    m_spriteRenderer[i].color = color;
                }
            }
            yield return null ;
        }
    }
}