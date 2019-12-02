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
	[Header("플레이어 추가 정보")]
	[SerializeField] private int maxCombo = 0;
	[SerializeField] private int airAttackCount = 0;
    [SerializeField] public float fMoveSpeed = 10.0f;
    [SerializeField] public float fJumpforce = 25.0f;

    public struct PlayerCharInfo
    {
        public int level;
        public int maxHp;
        public int attack;
        public int defensive;
        public int critical;
    }

	public struct PlayerCharEndInfo
	{
		public int maxCombo;
		public int airAttackCount;
	}

    public override int DamageCalculation(int _damage)
    {
        int returnDamage = _damage - defensive;
        if (returnDamage < 0)
            returnDamage = 0;
        return returnDamage;
    }

    public void SetInfo(PlayerCharInfo _charInfo)
    {
        level = _charInfo.level;
        maxHp = _charInfo.maxHp;
        hp = maxHp;
        attack = _charInfo.attack;
        defensive = _charInfo.defensive;
        critical = _charInfo.critical;
    }

    public void SetMaxCombo(int _maxCombo)
    {
        if(_maxCombo > maxCombo)
            maxCombo = _maxCombo;
    }

	public void PlusAirAttackCount()
	{
		airAttackCount++;
	}

	public PlayerCharEndInfo GetEndInfo()
	{
		PlayerCharEndInfo playerCharEndInfo;
		playerCharEndInfo.airAttackCount = this.airAttackCount;
		playerCharEndInfo.maxCombo = this.maxCombo;

		return playerCharEndInfo;
	}
}