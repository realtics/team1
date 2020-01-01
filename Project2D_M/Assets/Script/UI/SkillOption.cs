using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOption : MonoBehaviour
{
    private SkillOptionButton[] skillOptionButtons = null;

    private void Start()
    {
        skillOptionButtons = GetComponentsInChildren<SkillOptionButton>();

        string[] skillNames = SkillDataManager.Inst.GetSkillNames();
        for (int i = 0; i < skillOptionButtons.Length; ++i)
        {
            if (i >= skillNames.Length)
                break;

            skillOptionButtons[i].InitSkillInfo(skillNames[i]);
        }
    }
}
