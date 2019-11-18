using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyLoadSceneButton : MonoBehaviour
{
    enum MENU_INDEX
    {
        LOBBY_TO_INVENTORY,
        LOBBY_TO_WORLD_MAP,
        LOBBY_MENU_INDEX_END
    }

    public int thisMenuIndex;

    public void LoadSceneInLobby()
    {
        switch (thisMenuIndex)
        {
            case (int)MENU_INDEX.LOBBY_TO_INVENTORY:
                LoadingProgress.LoadScene("03_Inventory");
                break;

            case (int)MENU_INDEX.LOBBY_TO_WORLD_MAP:
                LoadingProgress.LoadScene("04_WorldMap");
                break;
            default:
                break;
        }
    }
}
