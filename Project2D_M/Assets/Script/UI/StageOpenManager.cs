using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageOpenManager : MonoBehaviour
{
	// Start is called before the first frame update

	public GameObject Stage2Lock;
	public GameObject Stage2Button;
    void Start()
    {

        if(StageDataManager.Inst.stageDataSO.maxStage == StageDataManager.StageNameEnum.STAGE_1_2)
		{
			Stage2Lock.SetActive(false);
			Stage2Button.GetComponent<Button>().interactable = true;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
