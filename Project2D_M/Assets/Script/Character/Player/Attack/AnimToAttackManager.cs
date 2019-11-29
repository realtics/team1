using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_29
 * 팀              : 1팀
 * 스크립트 용도   : 애니메이션이벤트, 관련 공격 콜라이더 연결
 */
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
