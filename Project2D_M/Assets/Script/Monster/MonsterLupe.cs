using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자             : 한승훈
 * 최종 수정 날짜     : 2019.11.14
 * 팀                 : 1팀
 * 스크립트 용도      : 몬스터 스크립트
 */

enum MONSTER_STATE
{
    HID,
    MOVE,
    ATTACK,
    STUN,
    DIE
}
public class MonsterLupe : MonoBehaviour
{
    private MONSTER_STATE m_eState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
