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
    [SerializeField] protected int maxHp = 10;
    [SerializeField] protected int hp = 10;
    [SerializeField] public int attack { get; protected set; } = 2;
    [SerializeField] protected int defensive = 1;

    public void HpDamage(int _damage)
    {
        hp -= _damage;
    }

    public virtual int DamageCalculation(int _damage)
    {
        int returnDamage = _damage - defensive;
        return returnDamage;
    }

    public bool IsCharacterDie()
    {
        if (hp <= 0)
            return true;

        return false;
    }
}
