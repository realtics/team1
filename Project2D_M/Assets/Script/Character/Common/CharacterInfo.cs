using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_15
 * 팀              : 1팀
 * 스크립트 용도   : 정보 스크립트의 부모 클래스
 */
public class CharacterInfo : MonoBehaviour
{
    [Header("캐릭터 정보")]
    [SerializeField] protected int level = 1;
    [SerializeField] protected int maxHp = 100;
    [SerializeField] protected int hp = 100;
    [SerializeField] public int attack = 15;
    [SerializeField] protected int defensive = 10;
    [SerializeField] protected int critical = 0;
	public bool bOverKill { get; protected set; } = false;

	public void HpDamage(int _damage)
    {
		hp -= _damage;

		if (hp <= 0)
		{
			if (maxHp / 10 * 3 < _damage)
				bOverKill = true;
			hp = 0;
		}
    }

    public virtual int DamageCalculation(int _damage)
    {
        int returnDamage = _damage - defensive;
        if (returnDamage < 0)
            return 0;
        return returnDamage;
    }

    public bool IsCharacterDie()
    {
        if (hp <= 0)
            return true;

        return false;
    }

    public bool IsCritical()
    {
        int randNum = Random.Range(1, 101);

        if (randNum <= critical)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetHP()
    {
        return hp;
    }

    public int GetMaxHP()
    {
        return maxHp;
    }

    public int GetMaxMp()
    {
        return 0;
    }
}
