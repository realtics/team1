using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemContainer : MonoBehaviour, IItemContainer
{
	public List<ItemSlot> itemSlots;

	public event Action<BaseItemSlot> MountingEvent;
	public event Action<BaseItemSlot> UnMountingEvent;
	protected virtual void OnValidate()
	{
		GetComponentsInChildren(includeInactive: true, result: itemSlots);
	}

	protected virtual void Start()
	{
		for (int i = 0; i < itemSlots.Count; i++)
		{
			itemSlots[i].MountingEvent += slot => EventHelper(slot, MountingEvent);
			itemSlots[i].UnMountingEvent += slot => EventHelper(slot, UnMountingEvent);
		}
	}

	private void EventHelper(BaseItemSlot _itemSlot, Action<BaseItemSlot> _action)
	{
		if(_action != null)
		{
			_action(_itemSlot);
		}
	}

	public virtual bool CanAddItem(Item _item, int _amount = 1)
	{
		int freeSpaces = 0;

		foreach (ItemSlot itemSlot in itemSlots)
		{
			if (itemSlot.Item == null || itemSlot.Item.ID == _item.ID)
			{
				freeSpaces += _item.MaximumStacks - itemSlot.Amount;
			}
		}
		return freeSpaces >= _amount;
	}

	public virtual bool AddItem(Item _item)
	{
		for (int i = 0; i < itemSlots.Count; i++)
		{
			if (itemSlots[i].CanAddStack(_item))
			{
				itemSlots[i].Item = _item;
				itemSlots[i].Amount++;
				return true;
			}
		}

		for (int i = 0; i < itemSlots.Count; i++)
		{
			if (itemSlots[i].Item == null)
			{
				itemSlots[i].Item = _item;
				itemSlots[i].Amount++;
				return true;
			}
		}
		return false;
	}

	public virtual bool RemoveItem(Item _item)
	{
		for (int i = 0; i < itemSlots.Count; i++)
		{
			if (itemSlots[i].Item == _item)
			{
				itemSlots[i].Amount--;
				return true;
			}
		}
		return false;
	}

	public virtual Item RemoveItem(string _itemID)
	{
		for (int i = 0; i < itemSlots.Count; i++)
		{
			Item item = itemSlots[i].Item;
			if (item != null && item.ID == _itemID)
			{
				itemSlots[i].Amount--;
				return item;
			}
		}
		return null;
	}

	public virtual int ItemCount(string _itemID)
	{
		int number = 0;

		for (int i = 0; i < itemSlots.Count; i++)
		{
			Item item = itemSlots[i].Item;
			if (item != null && item.ID == _itemID)
			{
				number += itemSlots[i].Amount;
			}
		}
		return number;
	}

	public void Clear()
	{
		for (int i = 0; i < itemSlots.Count; i++)
		{
			if (itemSlots[i].Item != null && Application.isPlaying)
			{
				itemSlots[i].Item.Destroy();
			}
			itemSlots[i].Item = null;
			itemSlots[i].Amount = 0;
		}
	}
}
