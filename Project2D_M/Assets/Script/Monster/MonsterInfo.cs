using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우 ,한승훈
 * 최종 수정 날짜  : 11_27
 * 팀              : 1팀
 * 스크립트 용도   : 몬스터의 정보 스크립트, hp계산 스크립트
 */
public class MonsterInfo : CharacterInfo
{
    [SerializeField]
    protected float m_fAttackDistance;
    [SerializeField]
    protected float m_fSpeed;

	public float speed
	{
		get
		{
			return m_fSpeed;
		}
	}

    public struct MonsterCharInfo
    {
        public int level;
        public int maxHp;
        public int attack;
        public int defensive;
        public float attackDistance;
		public float speed;
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
        m_fAttackDistance = _charInfo.attackDistance;
		m_fSpeed = _charInfo.speed;
    }

    public float GetAttackDistance()
    {
        return m_fAttackDistance;
    }

	
}