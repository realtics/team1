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
	[SerializeField]
    protected float m_attackDistance;

    public struct MonsterCharInfo
    {
        public int level;
        public int maxHp;
        public int attack;
        public int defensive;
        public float attackDistance;
    }

    public override int DamageCalculation(int _damage)
    {
        int returnDamage = _damage - defensive;
        if (returnDamage < 0)
            returnDamage = 0;
        return returnDamage;
    }

    public void SetInfo(MonsterCharInfo _charInfo)
    {
        level = _charInfo.level;
        maxHp = _charInfo.maxHp;
        hp = maxHp;
        attack = _charInfo.attack;
        defensive = _charInfo.defensive;
        m_attackDistance = _charInfo.attackDistance;
    }

    public float GetAttackDistance()
    {
        return m_attackDistance;
    }
}