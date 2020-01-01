using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillOptionButton : MonoBehaviour
{
    [SerializeField] private GameObject m_LockObject;
    [SerializeField] private Image m_skillImage;
    [SerializeField] private TextMeshProUGUI m_skillName;
    [SerializeField] private TextMeshProUGUI m_skillLevel;
    [SerializeField] private InfoText m_infoText;

    public void InitSkillInfo(string _skillName)
    {
        SkillDataManager.SkillInfo skillInfo = SkillDataManager.Inst.GetSkillInfo(_skillName);
        m_skillName.text = _skillName;
        m_skillImage.sprite = skillInfo.skillImage;
        m_skillLevel.text = "LV : " + PlayerDataManager.Inst.GetSkillLevelInfo(_skillName).ToString();

        if (PlayerDataManager.Inst.GetPlayerData().level >= skillInfo.levelLimit)
            m_LockObject.SetActive(false);
    }

    public void SkillLevelUp()
    {
        if (PlayerDataManager.Inst.GetPlayerData().gold < 500)
            return;

        PlayerDataManager.Inst.SavePlayerData(PlayerDataManager.PlayerData.DATA_ENUM.DATA_ENUM_GOLD, -500);
        PlayerDataManager.Inst.SkillLevelUp(m_skillName.text);
        m_skillLevel.text = "LV : " + PlayerDataManager.Inst.GetSkillLevelInfo(m_skillName.text).ToString();
        m_infoText.RefreshStatus();
    }
}
