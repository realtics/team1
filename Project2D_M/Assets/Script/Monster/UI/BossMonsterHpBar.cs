using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Spine.Unity;


public class BossMonsterHpBar : MonsterHpBar
{
	[SerializeField]
	protected Image hpBarImage1 = null;
	[SerializeField]
	protected Image damageBarImage1 = null;
	[SerializeField]
	protected Image hpBarImage2 = null;
	[SerializeField]
	protected Image damageBarImage2 = null;
	[SerializeField]
	protected TextMeshProUGUI hpText = null;
	[SerializeField]
	protected Canvas hpbarGroup1 = null;
	[SerializeField]
	protected Canvas hpbarGroup2 = null;
	[SerializeField]
	protected Image armorBarImage = null;
	[SerializeField]
	protected Image armorDamageBarImage = null;
	[SerializeField]
	protected SkeletonAnimation animArmorBreak = null;


	private PlayerInfo m_playerInfo = null;
	public bool bLastHp;
	//public bool bChangeBar = false;
	public int iCurValue;
	private float m_fArmorTime;
	private float m_fArmorAnimTime;

	private void Start()
	{
		bLastHp = false;

		hpBarImage1.fillAmount = 1f;
		hpBarImage2.fillAmount = 1f;
		damageBarImage1.fillAmount = 1f;
		damageBarImage2.fillAmount = 1f;
		hpBar = hpBarImage1;
		damageBar = damageBarImage1;
		hpBar.fillAmount = 1f;
		m_playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
		m_fArmorAnimTime = 1.0f;
	}

	public void InitInfo()
	{
		bLastHp = false;

		hpBarImage1.fillAmount = 1f;
		hpBarImage2.fillAmount = 1f;
		damageBarImage1.fillAmount = 1f;
		damageBarImage2.fillAmount = 1f;
		hpBar = hpBarImage1;
		damageBar = damageBarImage1;
		hpBar.fillAmount = 1f;
		m_playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
		m_fArmorAnimTime = 1.0f;
	}

    protected override void Update()
    {
        if(m_playerInfo.IsCharacterDie())
        {
            EndBossUI();
        }
        ArmorUpdate();
		ResetAnim();
		base.Update();
    }

    public void SetHPBar(float _maxHp, float _curHp)
    {
        hpBar.fillAmount = _curHp / _maxHp;
        if (m_fDamgeTime > 0)
            m_fDamgeTime = DAMAGE_MAX_TIME;
    }

    public void ResetHpBar()
    {
        hpText.SetText("x" + iCurValue.ToString());
        if (!bLastHp && iCurValue>1)
        {
            if (hpBar == hpBarImage1)
            {
                hpBarImage2.fillAmount = 1.0f;
                damageBarImage2.fillAmount = 1.0f;
            }
            else
            {
                hpBarImage1.fillAmount = 1.0f;
                damageBarImage1.fillAmount = 1.0f;
            }
        }        
    }

    public void ChangeHpBar()
    {
        if (hpBar == hpBarImage1)
        {
            hpBar = hpBarImage2;
            damageBar = damageBarImage2;
            hpbarGroup2.sortingOrder = 3;
        }
        else
        {
            hpBar = hpBarImage1;
            damageBar = damageBarImage1;
            hpbarGroup2.sortingOrder = 1;
        }
    }

    public void EndBossUI()
    {
        hpbarGroup1.sortingOrder = 0;
        hpbarGroup2.sortingOrder = 0;
    }

    public void HpZero()
    {
        if (hpBar == hpBarImage1)
        {
            hpBarImage2.fillAmount = 0f;
            damageBarImage2.fillAmount = 0f;
        }
        else
        {
            hpBarImage1.fillAmount = 0f;
            damageBarImage1.fillAmount = 0f;
        }
    }

    public void SetText()
    {
        hpText.SetText("x" + iCurValue.ToString());
    }

    public void ArmorUpdate()
    {
        m_fArmorTime -= Time.deltaTime;
        if (m_fArmorTime < 0)
        {
            if (armorBarImage.fillAmount < armorDamageBarImage.fillAmount)
            {
                float speed = 0.5f;
                armorDamageBarImage.fillAmount -= speed * Time.deltaTime;
            }
        }
    }

    public void SetArmorBar(float _maxArmor, float _curArmor)
    {
        armorBarImage.fillAmount = _curArmor / _maxArmor;
        if (m_fArmorTime > 0)
			m_fArmorTime = DAMAGE_MAX_TIME;
    }

	public void PlayAnimArmorBreak()
	{

		animArmorBreak.AnimationState.SetAnimation(0, "appear", false);
	}

	public void SyncAnimPosition(Transform _transform)
	{
		animArmorBreak.gameObject.SetActive(true);
		Vector3 tempVec = _transform.position;
		tempVec.y += 3;
		animArmorBreak.gameObject.transform.position = tempVec;
	}

	public void ResetAnim()
	{
		if(animArmorBreak.gameObject.activeSelf)
		{
			m_fArmorAnimTime -= Time.deltaTime;

			if(m_fArmorAnimTime<0)
			{
				animArmorBreak.gameObject.SetActive(false);
				m_fArmorAnimTime = 1.0f;
			}
		}
	}
}
