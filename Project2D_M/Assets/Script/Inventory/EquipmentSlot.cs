using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
	public EQUIPMENT_TYPE equipmentType;

	protected override void OnValidate()
	{
		base.OnValidate();
		gameObject.name = equipmentType.ToString() + " Slot";
	}

	
}
