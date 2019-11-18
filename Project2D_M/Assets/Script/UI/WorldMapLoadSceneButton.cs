using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapLoadSceneButton : MonoBehaviour
{
    enum MENU_INDEX
    {
        WORLDMAP_TO_LOBBY,
        WORLDMAP_TO_INVENTORY,
        WORLDMAP_MENU_INDEX_END
    }

    enum STAGE_INDEX
    {
        STAGE_01 =10,
        STAGE_02,
        STAGE_03,
        STAGE_04,
        STAGE_05

    }

    public int thisMenuIndex;

    public void LoadSceneInWorldMap()
    {
        switch (thisMenuIndex)
        {
            case (int)MENU_INDEX.WORLDMAP_TO_LOBBY:
                LoadingProgress.LoadScene("02_LobyScene");
                break;

            case (int)MENU_INDEX.WORLDMAP_TO_INVENTORY:
                LoadingProgress.LoadScene("03_Inventory");
                break;
            case (int)STAGE_INDEX.STAGE_01:
                LoadingProgress.LoadScene("Stage1");
                break;
            case (int)STAGE_INDEX.STAGE_02:
                LoadingProgress.LoadScene("");
                break;
            case (int)STAGE_INDEX.STAGE_03:
                LoadingProgress.LoadScene("");
                break;
            case (int)STAGE_INDEX.STAGE_04:
                LoadingProgress.LoadScene("");
                break;
            case (int)STAGE_INDEX.STAGE_05:
                LoadingProgress.LoadScene("");
                break;
            default:
                break;
        }
    }
}
