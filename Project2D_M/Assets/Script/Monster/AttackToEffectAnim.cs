using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_25
 * 팀              : 1팀
 * 스크립트 용도   : AttackManager에 연결해 원하는 애니메이션 이펙트를 진행시키기 위한 스크립트
 */
public class AttackToEffectAnim : MonoBehaviour
{
    public AttackManager attackManager;

    //애니메이션 이벤트 사용
    public void PlayAnim(string _animname, string _attackObjectName = null, bool _roof = false)
    {
        attackManager.PlayAnim(_animname, _attackObjectName, _roof);
    }
}
