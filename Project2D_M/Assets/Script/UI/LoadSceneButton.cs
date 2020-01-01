using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자         : 박종현
 * 최종 수정 날짜 : 2019.11.19
 * 팀             : 1 Team
 * 스크립트 용도  : 씬 이동용 버튼 공통 스크립트. (기존의 버튼 스크립트들 다 정리.)
 *                  인스펙터창에서 이동 원하는 씬 선택하고 GotoScene()를 클릭 이벤트에 넣어주기만하면된다.
 *                  스크립트를 간단하게 바꿧지만 빌드 순서에 맞추어 enum 순서를 정했기 때문에 주의 할 필요가있다.
*/

public class LoadSceneButton : MonoBehaviour
{
    public enum SCENE_NAME
    {
        TITLE,
        LOADING,
        LOBBY,
        INVENTORY,
        WORLDMAP,
        STAGE_1,
		STAGE_2,
		STAGE_3,
		STAGE_4
    }

    public SCENE_NAME wantToGoSceneName;

    public virtual void GotoScene()
    {
		LoadingProgress.LoadScene((int)wantToGoSceneName);
	}

    public void OnClickExit()
    {
        Application.Quit();
    }
}
