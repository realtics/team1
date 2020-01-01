using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireBall : MonoBehaviour, ISkillShoot
{
	public int pointCount = 4;
	public int distance = 10;
	public float speed = 1.5f;
	public float height = 0.5f;
    private GameObject[] fireBallObjects = new GameObject[4];
	private AudioFunction m_audioFunction = null;

    public void InitShoot(bool _xFilp, DamageInfo _damageInfo)
    {
		m_audioFunction = m_audioFunction ?? GetComponent<AudioFunction>();
		
		bool up = true;
        int num = 0;

        for (int i = 0; i < 4; ++i)
        {
            GameObject fireBallObject = ObjectPool.Inst.PopFromPool("FireBall");
            FireBallMove fireBallMove = fireBallObject.GetComponent<FireBallMove>();
            PlayerShootAttackCollider playerShootAttackCollider = fireBallObject.GetComponent<PlayerShootAttackCollider>();

            if (i % 2 == 0)
                num++;

            if ((_xFilp && _damageInfo.attackForce.x < 0) || (!_xFilp && _damageInfo.attackForce.x > 0))
            {
                _damageInfo.attackForce.x = _damageInfo.attackForce.x * -1;
            }

            playerShootAttackCollider.SetDamageColliderInfo(_damageInfo.damage, "Monster", _damageInfo.attackForce, this.gameObject);
            fireBallMove.InitFireBall(this.transform.position, distance, pointCount, height, up, _xFilp, num);
            up = !up;

            fireBallObjects[i] = fireBallObject;
        }
    }
    public void ShootAction()
	{
        for (int i = 0; i < 4; ++i)
        {
            fireBallObjects[i].SetActive(true);
		}

		m_audioFunction.AudioPlay("Start",false);
		StartCoroutine(nameof(AudioCoroutine));
    }

	private IEnumerator AudioCoroutine()
	{
		int count = fireBallObjects.Length;

		while (count > 0)
		{
			int actveNum = 0;
			for (int i = 0; i < 4; ++i)
			{
				if (fireBallObjects[i].activeInHierarchy)
					actveNum++;
			}

			if(actveNum != count)
			{
				count = actveNum;
				m_audioFunction.AudioPlay("End", false);
			}

			yield return null;
		}

		yield return null;
	}
}
