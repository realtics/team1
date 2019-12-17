using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SkillQuick : MonoBehaviour
{
	private SkillDataManager.SkillInfo m_skillInfo;
	private float coolTime;
	private float coolTimeLeft = 0;
	private PlayerInput m_playerInput = null;
	private bool m_bCool = false;
	private Image m_coolImage;
	private TextMeshProUGUI m_text;

	public void InitQuickSkill(string _skillname, PlayerInput _playerInput)
	{
		if (_skillname == null)
			return;

		m_skillInfo = SkillDataManager.Inst.GetSkillInfo(_skillname);
		this.GetComponentsInChildren<Image>()[1].enabled = true;
		this.GetComponentsInChildren<Image>()[1].sprite = m_skillInfo.skillImage;
		coolTime = m_skillInfo.coolTime;
		m_playerInput = _playerInput;
		m_coolImage = this.GetComponentsInChildren<Image>()[2];
		m_text = this.GetComponentInChildren<TextMeshProUGUI>();

		EventTrigger.Entry dounEvent = new EventTrigger.Entry();
		dounEvent.eventID = EventTriggerType.PointerDown;
		dounEvent.callback.AddListener(BaseEventData => SkillAction());

		this.GetComponentInChildren<EventTrigger>().triggers.Add(dounEvent);
	}

	private void SkillAction()
	{
		if (!m_bCool)
		{
			if (m_playerInput.SkillAction(m_skillInfo.skillName))
			{
				StartCoroutine(nameof(CoolTimeRecovery));
				m_bCool = true;
			}
		}
	}

	private IEnumerator CoolTimeRecovery()
	{
		m_text.text = coolTime.ToString();
		m_coolImage.fillAmount = 1.0f;

		while (coolTimeLeft <= coolTime)
		{
			yield return new WaitForSeconds(0.1f);
			coolTimeLeft += 0.1f;
			m_coolImage.fillAmount = 1.0f - (coolTimeLeft / coolTime);
			m_text.text = ((int)(coolTime - coolTimeLeft)+1).ToString();
		}

		m_text.text = "";
		coolTimeLeft = 0;
		m_coolImage.fillAmount = 0;

		m_bCool = false;
	}
}
