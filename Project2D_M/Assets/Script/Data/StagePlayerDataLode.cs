using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
