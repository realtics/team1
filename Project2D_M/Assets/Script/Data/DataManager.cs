using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using DataInfo;

public class DataManager : MonoBehaviour
{
    private string m_dataPath;
    // Start is called before the first frame update

    public void Initialize()
    {
        m_dataPath = Application.persistentDataPath + "/gameData.dat";
    }
    public void Save(GameData gameData)
    {
        //바이너리 파일 포맷을 위한 BinaryFormatter 생성
        BinaryFormatter bf = new BinaryFormatter();
        //데이터 저장을 위한 파일 생성
        FileStream file = File.Create(m_dataPath);
        //파일에 저장할 클래스에 데이터 할당
        GameData data = new GameData();
        data.killCount = gameData.killCount;
        data.hp = gameData.hp;
        data.speed = gameData.speed;
        data.damage = gameData.damage;
        data.equipItem = gameData.equipItem;
        //BinaryFormatter를 사용해 파일에 데이터 기록
        bf.Serialize(file, data);
        file.Close();
    }

    //파일에서 데이터를 추출하는 함수
    public GameData Load()
    {
        if (File.Exists(m_dataPath))
        {
            //파일이 존재할 경우 데이터 불러오기
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(m_dataPath, FileMode.Open);
            //GameData 클래스에 파일로부터 읽은 데이터를 기록
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();
            return data;
        }
        else
        {
            //파일이 없을 경우 기본값을 반환
            GameData data = new GameData();
            return data;
        }
    }
}
