using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageOpenManager : MonoBehaviour
{
	// Start is called before the first frame update

	public GameObject Stage2Lock;
	public GameObject Stage2Button;
	public GameObject Stage3Lock;
	public GameObject Stage3Button;
	public GameObject Stage4Lock;
	public GameObject Stage4Button;
	void Start()
    {

        if((int)StageDataManager.Inst.stageDataSO.maxStage >= (int)StageDataManager.StageNameEnum.STAGE_1_2)
		{
			Stage2Lock.SetActive(false);
			Stage2Button.GetComponent<Button>().interactable = true;
		}
		if ((int)StageDataManager.Inst.stageDataSO.maxStage >= (int)StageDataManager.StageNameEnum.STAGE_1_3)
		{
			Stage3Lock.SetActive(false);
			Stage3Button.GetComponent<Button>().interactable = true;
		}
		if ((int)StageDataManager.Inst.stageDataSO.maxStage >= (int)StageDataManager.StageNameEnum.STAGE_1_4)
		{
			Stage4Lock.SetActive(false);
			Stage4Button.GetComponent<Button>().interactable = true;
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
