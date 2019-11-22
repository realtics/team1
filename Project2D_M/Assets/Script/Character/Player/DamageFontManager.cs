using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Sprite[] criticalDamageFont;
    public Sprite[] normalDamageFont;

    private DamageFont.DamageFontOption damageFontOption;

    private void Awake()
    {
        damageFontOption.criticalMark = criticalMark;
        damageFontOption.criticalDamageFont = criticalDamageFont;
        damageFontOption.normalDamageFont = normalDamageFont;

        damageFontOption.fontSize = fontSize;
        damageFontOption.fontSpace = fontSpace / 100;
        damageFontOption.upSpeed = upSpeed / 100;
        damageFontOption.clearSpeed = clearSpeed / 100;

    }
    public void ShowDamage(int _damage, Vector3 _position, bool _bCritical = false)
    {
        GameObject damgeFontObj = ObjectPool.Inst.PopFromPool(damageFontName);
        damgeFontObj.SetActive(true);
        damgeFontObj.transform.position = _position;

        DamageFont damageFont = damgeFontObj.GetComponent<DamageFont>();
        damageFont.DamageFontInit(damageFontOption);
        damageFont.SetDamage(_damage, _bCritical);
    }
}
