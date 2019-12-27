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
    public SkillManager skillManager;

    //애니메이션 이벤트 사용
    public void PlayAnimToSkill(string _animname)
    {
		skillManager.PlayAnim(_animname);
    }
}
