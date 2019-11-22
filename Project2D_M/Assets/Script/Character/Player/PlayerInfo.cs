using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_15
 * 팀              : 1팀
 * 스크립트 용도   : 플레이어의 정보 스크립트, hp계산 스크립트
 */
public class PlayerInfo : CharacterInfo
{
    //[Header("플레이어 추가 정보")]

    public struct playerCharInfo
    {
        public int maxHp;
        public int attack;
        public int defensive;
        public int critical;
    }

    public override int DamageCalculation(int _damage)
    {
        int returnDamage = _damage - defensive;
        return returnDamage;
    }

    public void SetInfo(playerCharInfo _charInfo)
    {
        maxHp = _charInfo.maxHp;
        hp = maxHp;
        attack = _charInfo.attack;
        defensive = _charInfo.defensive;
        critical = _charInfo.critical;
    }
}