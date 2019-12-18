using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCrecentFlame : MonoBehaviour, ISkillShoot
{
	public int objectCount = 6;
	public int distance = 10;
	public float boomCreateSpeed = 0.1f;

	public void ShootAction(bool _xFilp, DamageInfo _damageInfo)
	{
		StartCoroutine(ActionCoroutine(_xFilp, _damageInfo));
	}

	private IEnumerator ActionCoroutine(bool _xFilp, DamageInfo _damageInfo)
	{
		for (int i = 0; i < 6; ++i)
		{
			GameObject crecentFlameObject = ObjectPool.Inst.PopFromPool("FireBoom");
			CrecentFlameCtrl crecentFlameCtrl = crecentFlameObject.GetComponent<CrecentFlameCtrl>();
			PlayerShootAttackCollider playerShootAttackCollider = crecentFlameObject.GetComponent<PlayerShootAttackCollider>();

			Vector3 position = this.transform.position;

			if (i % 2 == 0)
				position.x = position.x + ((i * 0.5f + 1) * distance);
			else
				position.x = position.x - ((i * 0.5f + 1) * distance);

			crecentFlameObject.transform.position = position;

			Vector3 scale = crecentFlameObject.transform.localScale;

			if ((i % 2 != 0 && scale.x > 0) || (i % 2 == 0 && scale.x < 0))
			{
				scale.x = scale.x * -1;
				crecentFlameObject.transform.localScale = scale;
			}

			if ((i % 2 == 0 && _damageInfo.attackForce.x < 0) || (i % 2 != 0 && _damageInfo.attackForce.x > 0))
			{
				_damageInfo.attackForce.x = _damageInfo.attackForce.x * -1;
			}

			playerShootAttackCollider.SetDamageColliderInfo(_damageInfo.damage, "Monster", _damageInfo.attackForce, this.gameObject, false);
			crecentFlameCtrl.InitCrecentFlame();


			yield return new WaitForSeconds(boomCreateSpeed);
		}
	}
}
