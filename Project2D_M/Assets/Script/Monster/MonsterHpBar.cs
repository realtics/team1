using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : MonoBehaviour
{
    [SerializeField]
    private Image hpbar = null;
    private MonsterInfo m_monsterInfo;
    
    public void SetHPBar(MonsterInfo _info)
    {
        float maxhp = _info.GetMaxHP(); ;
        float hp = _info.GetHP();

        hpbar.fillAmount = hp / maxhp;
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
