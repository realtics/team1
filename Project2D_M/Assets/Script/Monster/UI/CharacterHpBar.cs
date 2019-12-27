using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * 작성자          : 한승훈
 * 최종 수정 날짜  : 11.27
 * 팀              : 1팀
 * 스크립트 용도   : 인게임에서 캐릭터 hpbar
 */
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
