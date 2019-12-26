using UnityEngine;

public static class ItemSaveIO
{
	private static readonly string baseSavePath;

	static ItemSaveIO()
	{
		baseSavePath = Application.persistentDataPath;
	}

	public static void SaveItems(ItemContainerSaveData _items, string _fileName)
	{
		ItemFileReadWrite.WriteToBinaryFile(baseSavePath + "/" + _fileName + ".dat",_items);
	}

	public static ItemContainerSaveData LoadItems(string _fileName)
	{
		string filePath = baseSavePath + "/" + _fileName + ".dat";

		if(System.IO.File.Exists(filePath))
		{
			return ItemFileReadWrite.ReadFromBinaryFile<ItemContainerSaveData>(filePath);
		}

		return null;
	}

}
