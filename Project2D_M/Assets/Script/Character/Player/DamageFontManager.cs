using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 작성자          : 고은우
 * 최종 수정 날짜  : 11_25
 * 팀              : 1팀
 * 스크립트 용도   : 데미지폰트의 크기,이미지등 관련 속성과 오브젝트 풀에서의 가져오는 역활
 */
public class DamageFontManager : Singletone<DamageFontManager>
{

    [Header("폰트 설정")]
    public string damageFontName = "DamageFont";
    private DamageFont damageFont;
    public Vector2 fontSize;
    public float fontSpace;
    public float upSpeed;
    public float clearSpeed;

    [Header("데미지 폰트 스프라이트")]
    public Sprite criticalMark;
	public Sprite missMark;
    public Sprite[] criticalDamageFont;
    public Sprite[] normalDamageFont;

    private DamageFont.DamageFontOption damageFontOption;

    private void Awake()
    {
        damageFontOption.criticalMark = criticalMark;
        damageFontOption.criticalDamageFont = criticalDamageFont;
		damageFontOption.missMark = missMark;

		damageFontOption.normalDamageFont = normalDamageFont;

        damageFontOption.fontSize = fontSize;
        damageFontOption.fontSpace = fontSpace / 100;
        damageFontOption.upSpeed = upSpeed / 100;
        damageFontOption.clearSpeed = clearSpeed / 100;

    }
    public void ShowDamage(int _damage, Vector3 _position, bool _bCritical)
    {
        GameObject damgeFontObj = ObjectPool.Inst.PopFromPool(damageFontName);
        damgeFontObj.SetActive(true);
        damgeFontObj.transform.position = _position;

        DamageFont damageFont = damgeFontObj.GetComponent<DamageFont>();
        damageFont.DamageFontInit(damageFontOption);
        damageFont.SetDamage(_damage, _bCritical);
    }
}
