using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_25
 * 팀              : 1팀
 * 스크립트 용도   : 플레이어 데이터를 player오브젝트에 입력하기 위한 스크립트
 */
public class StagePlayerDataLode : MonoBehaviour
{
    public PlayerInfo playerInfo;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInfo.PlayerCharInfo playerCharInfo;
        PlayerDataManager.PlayerData playerData = PlayerDataManager.Inst.GetPlayerData();
        playerCharInfo.attack = playerData.attack;
        playerCharInfo.defensive = playerData.defensive;
        playerCharInfo.critical = playerData.critical;
        playerCharInfo.maxHp = playerData.maxHp;
        playerCharInfo.level = playerData.level;

        playerInfo.SetInfo(playerCharInfo);
    }
}
