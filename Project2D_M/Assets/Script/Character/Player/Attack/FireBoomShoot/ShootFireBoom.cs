using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireBoom : MonoBehaviour, ISkillShoot
{
	public int objectCount = 3;
	public int distance = 10;
	public float boomSpeed = 1.5f;
	public float boomCreateSpeed = 0.1f;
    public FireBoomCtrl[] fireBoomCtrls = new FireBoomCtrl[3];

    public void InitShoot(bool _xFilp, DamageInfo _damageInfo)
    {
        for (int i = 0; i < 3; ++i)
        {
            GameObject fireBoomObject = ObjectPool.Inst.PopFromPool("FireBoom");
            FireBoomCtrl fireBoomCtrl = fireBoomObject.GetComponent<FireBoomCtrl>();
            PlayerShootAttackCollider playerShootAttackCollider = fireBoomObject.GetComponent<PlayerShootAttackCollider>();

            if (_xFilp)
                fireBoomObject.transform.position = new Vector3(this.transform.position.x + ((i + 1) * distance), this.transform.position.y, this.transform.position.z);
            else
                fireBoomObject.transform.position = new Vector3(this.transform.position.x - ((i + 1) * distance), this.transform.position.y, this.transform.position.z);

            if ((_xFilp && _damageInfo.attackForce.x < 0) || (!_xFilp && _damageInfo.attackForce.x > 0))
            {
                _damageInfo.attackForce.x = _damageInfo.attackForce.x * -1;
            }

            playerShootAttackCollider.SetDamageColliderInfo(_damageInfo.damage, "Monster", _damageInfo.attackForce, this.gameObject, false);
            fireBoomCtrl.InitFireBoom(boomSpeed);

            fireBoomCtrls[i] = fireBoomCtrl;
        }
    }

    public void ShootAction()
	{
		StartCoroutine(ActionCoroutine());
	}

	private IEnumerator ActionCoroutine()
	{
		for (int i = 0; i < 3; ++i)
		{
            fireBoomCtrls[i].ShootBoom();

			yield return new WaitForSeconds(boomCreateSpeed);
		}
	}
}
