using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSaveData;
using GameSaveDataIO;

public class ItemDataManager : Singletone<ItemDataManager>
{
    public string dataname = "ItemData.dat";
    public ItemDataScriptableObject dataIO;

    public class ItemData
    {
        public enum DATA_ENUM
        {

        }

        public Dictionary<string, ItemInfoData> itemDic = new Dictionary<string, ItemInfoData>();

    }

    private ItemData m_itemData = null;
    private ItemSaveData m_itemSaveData = null;

    private void Awake()
    {
        m_itemSaveData = BinaryManager.Load<ItemSaveData>(dataname);

        if (m_itemSaveData == null)
        {
            Debug.Log("아이템 정보 로드 실패.");
            return;
        }

        if (dataIO == null)
            dataIO = (ItemDataScriptableObject)Resources.Load("Data/ItemDataScript");


        BinaryManager.Save(m_itemSaveData, dataname);
    }

}
