using UnityEngine;

public static class ItemSaveIO
{
	private static readonly string baseSavePath;

	static ItemSaveIO()
	{
		baseSavePath = Application.persistentDataPath;
	}

	public static void SaveItems(ItemContainerSaveData _items, string _path)
	{
		ItemFileReadWrite.WriteToBinaryFile(baseSavePath + "/" + _path + ".dat",_items);
	}

	public static ItemContainerSaveData LoadItems(string _path)
	{
		string filePath = baseSavePath + "/" + _path + ".dat";

		if(System.IO.File.Exists(filePath))
		{
			return ItemFileReadWrite.ReadFromBinaryFile<ItemContainerSaveData>(filePath);
		}

		return null;
	}

}
