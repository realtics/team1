using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHpBar : MonsterHpBar
{
    [SerializeField]
    private Image mpbar = null;

    public void SetMPBar(MonsterInfo _info)
    {
        //float maxhp = _info.GetMaxHP(); ;
        //float hp = _info.GetHP();

        //hpbar.fillAmount = hp / maxhp;
        return;
    }
}
