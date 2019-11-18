using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLoadSceneButton : MonoBehaviour
{
    enum MENU_INDEX
    {
        INVENTORY_TO_LOBBY,
        INVENTORY_TO_SMITHY,
        INVENTORY_MENU_INDEX_END
    }

    public int thisMenuIndex;

    public void LoadSceneInInventory()
    {
        switch (thisMenuIndex)
        {
            case (int)MENU_INDEX.INVENTORY_TO_LOBBY:
                LoadingProgress.LoadScene("02_LobyScene");
                break;

            case (int)MENU_INDEX.INVENTORY_TO_SMITHY:
                LoadingProgress.LoadScene("");
                break;
            default:
                break;
        }
    }
}
