using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_25
 * 팀              : 1팀
 * 스크립트 용도   : 바이너리 데이터의 저장과 로드를 제네릭으로 묶은 static함수 스크립트
 */
public class BinaryManager
{
    public static void Save<T>(T _data, string _dataPath)
    { 
        //바이너리 파일 포맷을 위한 BinaryFormatter 생성
        BinaryFormatter bf = new BinaryFormatter();
        //데이터 저장을 위한 파일 생성
        FileStream file = File.Create(Application.persistentDataPath + "/" +  _dataPath);
        bf.Serialize(file, _data);
        file.Close();
    }

    //파일에서 데이터를 추출하는 함수
    public static T Load<T>(string _dataPath)
    {
        if (File.Exists(Application.persistentDataPath + "/" + _dataPath))
        {
            //파일이 존재할 경우 데이터 불러오기
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + _dataPath, FileMode.Open);
            //GameData 클래스에 파일로부터 읽은 데이터를 기록
            T data = (T)bf.Deserialize(file);
            file.Close();
            return data;
        }
        else
        {
            return default;
        }
    }
}
