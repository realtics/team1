using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimToAttackManager : MonoBehaviour
{
    public AttackManager attackManager;

    //애니메이션 이벤트 사용
    public void ColliderLifeCycleOn(float _time)
    {
        attackManager.ColliderLifeCycleOn(_time);
    }

    //애니메이션 이벤트 사용
    public void PlayAnim(string _animname)
    {
        attackManager.PlayAnim(_animname);
    }
}
