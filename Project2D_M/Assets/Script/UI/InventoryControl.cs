using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 작성자         : 박종현
 * 최종 수정 날짜 : 2019.11.14
 * 팀             : 1 Team
 * 스크립트 용도  : 인벤토리 칸을 동적으로 생성시켜주기위한 임시 스크립트. 현재는 임시로 100으로 설정해두었으나 추후에 연계과정에서 바꿀예정.
*/
public class InventoryControl : MonoBehaviour
{
    private int m_iMaxHorizontalLength = 6;

    private List<PlayerItem> m_PlayerItemList;

    [SerializeField]
    private GameObject m_objButtonTemplate;

    [SerializeField]
    private GridLayoutGroup m_gridGroup;

    [SerializeField]
    private Sprite[] m_IconSprites;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerItemList = new List<PlayerItem>();

        for(int i = 1; i <=100; i++)
        {
            PlayerItem newItem = new PlayerItem();
            newItem.iconSprite = m_IconSprites[0];

            m_PlayerItemList.Add(newItem);
        }

        GetInventory();
    }

    void GetInventory()
    {
        if(m_PlayerItemList.Count < m_iMaxHorizontalLength)
        {
            m_gridGroup.constraintCount = m_PlayerItemList.Count;
        }
        else
        {
            m_gridGroup.constraintCount = m_iMaxHorizontalLength - 1;
        }

        foreach (PlayerItem newItem in m_PlayerItemList)
        {
            GameObject newButton = Instantiate(m_objButtonTemplate) as GameObject;
            newButton.SetActive(true);

            newButton.GetComponent<InventoryButton>().SetIcon(newItem.iconSprite);
            newButton.transform.SetParent(m_objButtonTemplate.transform.parent, false);
        }
    }


    public class PlayerItem
    {
        public Sprite iconSprite;
    }
}
