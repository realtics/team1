using UnityEngine;

public enum EQUIPMENT_TYPE
{
	WEAPON,
	HAT,
	TOP,
	GLOVES,
	PANTS,
	SHOES,
	NECKLACE,
	EARRING_1,
	EARRING_2,
	RING_1,
	RING_2,
}

[CreateAssetMenu]
public class EquippableItem : Item
{
	public int attackBonus;
	public int armorBonus;
	public int maxHealthBonus;
	[Space]
	public float attackPercentBonus;
	public float armorPercentBonus;
	public float maxHealthPercentBonus;
	[Space]
	public EQUIPMENT_TYPE equipmentType;
}
