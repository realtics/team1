using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_15
 * 팀              : 1팀
 * 스크립트 용도   : 몬스터의 정보 스크립트, hp계산 스크립트
 */
public class MonsterInfo : CharacterInfo
{
    // Start is called before the first frame update
    public class MonsterCharInfo : CharInfo
    {
    }

    public override int DamageCalculation(int _damage)
    {
        int returnDamage = _damage - defensive;
        return returnDamage;
    }

    public void SetInfo(MonsterCharInfo _charInfo)
    {
        maxHp = _charInfo.maxHp;
        hp = maxHp;
        attack = _charInfo.attack;
        defensive = _charInfo.defensive;
    }
}
