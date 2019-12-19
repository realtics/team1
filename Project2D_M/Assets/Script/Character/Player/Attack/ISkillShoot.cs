using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageInfo
{
	public int damage;
	public Vector2 attackForce;
}

public interface ISkillShoot
{
    void InitShoot(bool _xFilp, DamageInfo _damageInfo);
    void ShootAction();
}