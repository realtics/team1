using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackToEffectAnim : MonoBehaviour
{
    public AttackManager attackManager;

    //애니메이션 이벤트 사용
    public void PlayAnim(string _animname, string _attackObjectName = null, bool _roof = false)
    {
        attackManager.PlayAnim(_animname, _attackObjectName, _roof);
    }
}
