using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireBall : MonoBehaviour,ISkillShoot
{
	public int pointCount = 4;
	public int distance = 10;
	public float speed = 1.5f;
	public float height = 0.5f;

	public void ShootAction(bool _xFilp, DamageInfo _damageInfo)
	{
		bool up = true;
		int num = 0;

		for (int i = 0; i < 4; ++i)
		{
			GameObject fireBallObject = ObjectPool.Inst.PopFromPool("FireBall");
			FireBallMove fireBallMove = fireBallObject.GetComponent<FireBallMove>();
			PlayerShootAttackCollider playerShootAttackCollider = fireBallObject.GetComponent<PlayerShootAttackCollider>();
			fireBallObject.transform.position = this.transform.position;

			if (i % 2 == 0)
				num++;

			playerShootAttackCollider.SetDamageColliderInfo(_damageInfo.damage, "Monster", _damageInfo.attackForce,this.gameObject);
			fireBallMove.InitFireBall(fireBallObject.transform.position, distance, pointCount, height, up, _xFilp, num);
			up = !up;

			fireBallObject.SetActive(true);
		}
	}
}
