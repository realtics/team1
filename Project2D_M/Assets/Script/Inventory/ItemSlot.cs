using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
	[SerializeField] Image itemImage;
	[SerializeField] Image frameBackImage;
	[SerializeField] Image frameBackEdgeImage;
	[SerializeField] Image mountingImage;

	private Item m_item;

	public Item Item
	{
		get { return m_item; }
		set
		{
			m_item = value;

			if(m_item == null)
			{
				itemImage.enabled = false;
				//frameBackImage.enabled = false;
				frameBackEdgeImage.enabled = false;
				mountingImage.enabled = false;
			}
			else
			{
				itemImage.sprite = m_item.Icon;
				itemImage.enabled = true;
			}
		}
	}


	private void OnValidate()
	{
		SlotRefreshUI();
	}

	private void SlotRefreshUI()
	{
		if (frameBackImage == null)
		{
			frameBackImage = transform.GetChild(0).gameObject.GetComponent<Image>();
		}

		if (frameBackEdgeImage == null)
		{
			frameBackEdgeImage = transform.GetChild(1).gameObject.GetComponent<Image>();
		}

		if (itemImage == null)
		{
			itemImage = transform.GetChild(2).gameObject.GetComponent<Image>();
		}

		if (mountingImage == null)
		{
			mountingImage = transform.GetChild(3).gameObject.GetComponent<Image>();
		}
	}
}
