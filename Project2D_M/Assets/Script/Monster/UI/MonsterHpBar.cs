using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : MonoBehaviour
{
    [SerializeField]
    protected Image hpBar = null;
    [SerializeField]
    protected Image mainHpBar = null;
    [SerializeField]
    protected Image damageBar = null;

    protected float m_fDamgeTime;
    protected const float DAMAGE_MAX_TIME = 1.0f;

    protected virtual void Update()
    {
        m_fDamgeTime -= Time.deltaTime;
        if(m_fDamgeTime<0)
        {
            if(hpBar.fillAmount < damageBar.fillAmount)
            {
                float speed = 0.5f;
                damageBar.fillAmount -= speed * Time.deltaTime;
            }
        }
    }

    public void SetHPBar(CharacterInfo _info)
    {
        float maxhp = _info.GetMaxHP(); ;
        float hp = _info.GetHP();

        hpBar.fillAmount = hp / maxhp;
        if(m_fDamgeTime >0)
            m_fDamgeTime = DAMAGE_MAX_TIME;
        if (mainHpBar != null)
            mainHpBar.fillAmount = hp / maxhp;
    }

    public void SetHpBarDirection(float _x)
    {
        Vector3 a = this.transform.localScale;
        if(_x>0)
        {
            a.x = 1;
        }
        else
        {
            a.x = -1;
        }
        this.transform.localScale = a;
    }
}
