using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
 * 작성자         : 박종현
 * 최종 수정 날짜 : 2019.11.13
 * 팀             : 1 Team
 * 스크립트 용도  : 리소스(아틀라스) 중 원하는 이미지를 따로 떼어놓기 위해 만들어둔 스크립트. 추후에 원하는 경로에 저장할 예정.
*/

public class AtlasSeparator : MonoBehaviour
{
    public Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();

   //Resoures폴더 안에서 분리하고싶은 아틀라스가 저장된 폴더 이름을 적어준다. (폴더안에 있는 (슬라이스 된)아틀라스 모두 잘라줌)
   [SerializeField] private string m_strFolderName = null;

    private void Start()
    {
        LoadSprite(m_strFolderName);
    }

    public void LoadSprite(string _spritebasename)
    {
        Sprite[] allSprites = Resources.LoadAll<Sprite>(_spritebasename);

        if (allSprites == null || allSprites.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < allSprites.Length; i++)
        {
            spriteDic.Add(allSprites[i].name, allSprites[i]);
            MakeFile(allSprites[i]);
        }
    }

    void MakeFile(Sprite _sprite)
    {
        try
        {

            //분리할 스프라이트의 시작 좌표와 사이즈
            Rect textureRect = _sprite.rect;

            //스프라이트의 메인 텍스쳐를 가져옴
            Texture2D mainTexture = _sprite.texture;

            //새로 만들어질 텍스쳐, sprite.texture.format 이건 메인 텍스쳐의 포맷을 그대로 사용
            Texture2D newTexture = new Texture2D((int)textureRect.width, (int)textureRect.height, _sprite.texture.format, false);

            //계속 함수 GetPixels을 못받는 현상이 있었는데
            //이유가 <<해당 텍스쳐는 import 설정에서 Read/Write Enabled flag가 설정되어 있어야 합니다. 그렇지 않으면 함수는 실패합니다.>> (유니티 도큐먼트 참조)
            //이유로 해당 텍스쳐 옵션에서 Read/Write Enabled  체크 해줘야한다..
            
            //메인 텍스쳐에서 스프라이트의 영역 만큼 픽셀 값을 가져옴
            Color[] color = mainTexture.GetPixels((int)textureRect.x, (int)textureRect.y, (int)textureRect.width, (int)textureRect.height);

            newTexture.SetPixels(color);// 새 텍스쳐에 픽셀값을 입힘
            newTexture.Apply(); // 적용

            // PNG byte로 형태로 만듬. JPG는 EncodeToJPG 사용 
            var bytes = newTexture.EncodeToPNG();

            //저장할 파일 위치
            string savePath = string.Format("{0}/{1}.png", Application.persistentDataPath, _sprite.name);

            Object.DestroyImmediate(newTexture, true); //새텍스쳐는 쓸일이 없으므로 삭제
            System.IO.File.WriteAllBytes(savePath, bytes); //파일로 쓰기
        }
        catch (System.Exception)
        {

        }

    }
}
