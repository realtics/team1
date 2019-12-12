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

public enum ITEM_RATING
{
	NOMAL,
	MAGIC,
	RARE,
	EPIC,
	UNIQUE,
}
public enum ITEM_TYPE
{
	WEAPON,
	ARMOR,
	ACCESSORIES,
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
	public ITEM_RATING itemRating;
	public ITEM_TYPE itemType;
	[Space]
	public string ratingName;
	public string typeName;

	public void OnValidate()
	{
		switch (itemRating)
		{
			case ITEM_RATING.NOMAL:
				ratingName = "일반";
				frameColorRGB = new Vector3(79, 79, 79);
				break;
			case ITEM_RATING.MAGIC:
				ratingName = "매직";
				frameColorRGB = new Vector3(68, 102, 28);
				break;
			case ITEM_RATING.RARE:
				ratingName = "레어";
				frameColorRGB = new Vector3(74, 126, 194);
				break;
			case ITEM_RATING.EPIC:
				ratingName = "에픽";
				frameColorRGB = new Vector3(76, 22, 117);
				break;
			case ITEM_RATING.UNIQUE:
				ratingName = "유니크";
				frameColorRGB = new Vector3(184, 146, 38);
				break;
		}

		switch (itemType)
		{
			case ITEM_TYPE.WEAPON:
				typeName = "무기";
				break;
			case ITEM_TYPE.ARMOR:
				typeName = "방어구";
				break;
			case ITEM_TYPE.ACCESSORIES:
				typeName = "악세서리";
				break;
		}
	}

}
