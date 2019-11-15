using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
 * 작성자             : 한승훈
 * 최종 수정 날짜     : 2019.11.14
 * 팀                 : 1팀
 * 스크립트 용도      : 몬스터 AI스크립트
 */


public class MonsterLupe : MonoBehaviour

{
    private MONSTER_STATE   m_eState;
    private MonsterInfo     m_monsterInfo;
    private bool m_bLive;

    

    private void Awake()
    {
        //몬스터 유저 찾기 2가지 방법중
        //1) 어웨이크에서 유저 오브젝트를 저장 해놓고 좌표 거리계산으로 유저 찾기 << 선택
        //2) 탐색 콜리전으로 크게 콜리젼만들고 콜리젼 안에 들어오면 찾기
        var plyaer = GameObject.FindGameObjectWithTag("Player");

        MonsterInfo.MonsterCharInfo monsterCharInfo = new MonsterInfo.MonsterCharInfo();
        monsterCharInfo.maxHp = 40;
        monsterCharInfo.defensive = 10;
        monsterCharInfo.attack = 10;
        m_monsterInfo.SetInfo(monsterCharInfo);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
