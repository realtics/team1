using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCrecentFlame : MonoBehaviour, ISkillShoot
{
	public int distance = 10;
	public float createSpeed = 0.1f;
    private CrecentFlameCtrl[] crecentFlameCtrls = new CrecentFlameCtrl[6];

    public void InitShoot(bool _xFilp, DamageInfo _damageInfo)
    {
        StartCoroutine(InitCoroutine(_xFilp, _damageInfo));
    }

	public void ShootAction()
	{
		StartCoroutine(ActionCoroutine());
	}
    private IEnumerator ActionCoroutine()
    {
        for (int i = 0; i < 6; ++i)
        {
            crecentFlameCtrls[i].SkillAction();

            if (i % 2 == 0)
                yield return null;
            else
                yield return new WaitForSeconds(createSpeed);
        }
    }
    private IEnumerator InitCoroutine(bool _xFilp, DamageInfo _damageInfo)
    {
        for (int i = 0; i < 6; ++i)
        {
            GameObject crecentFlameObject = ObjectPool.Inst.PopFromPool("CrecentFlame");
            CrecentFlameCtrl crecentFlameCtrl = crecentFlameObject.GetComponent<CrecentFlameCtrl>();
            PlayerShootAttackCollider playerShootAttackCollider = crecentFlameObject.GetComponent<PlayerShootAttackCollider>();

            Vector3 position = this.transform.position;

            if (i % 2 == 0)
                position.x = position.x + distance + ((int)(i * 0.5f + 1) * distance * (int)(i * 0.5f));
            else
                position.x = position.x - distance - ((int)(i * 0.5f + 1) * distance * (int)(i * 0.5f));

            crecentFlameObject.transform.position = position;

            Vector3 scale = crecentFlameObject.transform.localScale * ((int)(i * 0.5f) + 1);

            if ((i % 2 != 0 && scale.x > 0) || (i % 2 == 0 && scale.x < 0))
            {
                scale.x = scale.x * -1;
            }

            crecentFlameObject.transform.localScale = scale;

            if ((i % 2 == 0 && _damageInfo.attackForce.x < 0) || (i % 2 != 0 && _damageInfo.attackForce.x > 0))
            {
                _damageInfo.attackForce.x = _damageInfo.attackForce.x * -1;
            }

            playerShootAttackCollider.SetDamageColliderInfo(_damageInfo.damage, "Monster", _damageInfo.attackForce, this.gameObject, false);
            crecentFlameCtrl.InitCrecentFlame();
            crecentFlameCtrls[i] = crecentFlameCtrl;

            if (i % 2 == 0)
                yield return null;
            else
                yield return new WaitForSeconds(createSpeed);
        }
    }
}
