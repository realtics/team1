using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ItemFileReadWrite 
{
   public static void WriteToBinaryFile<T>(string _filePath , T _objectToWrite)
   {
		using (Stream stream = File.Open(_filePath, FileMode.Create))
		{
			var binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(stream, _objectToWrite);
			stream.Close();
		}
   }
	public static T ReadFromBinaryFile<T>(string _filePath)
	{
		using (Stream stream = File.Open(_filePath, FileMode.Open))
		{
			var binaryFormatter = new BinaryFormatter();
			return (T)binaryFormatter.Deserialize(stream);
		}

	}
}