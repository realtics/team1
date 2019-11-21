using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFontManager : MonoBehaviour
{
    [SerializeField] GameObject damageFontPrab;

    Queue<GameObject> damageFontFool;

    public Sprite[] criticalDamageFont;
    private DamageFont damageFont;
    public int damage;
    public Vector2 fontSize;
    public float fontSpace;
    private void Awake()
    {
        for(int i = 0; i < 5; ++i)
        {
            Instantiate(damageFontPrab,this.transform);
        }
        
    }

    public void ShowDamage()
    {
        damageFont = this.GetComponentInChildren<DamageFont>();
        damageFont.DamageFontInit(criticalDamageFont, fontSize, fontSpace);
        damageFont.SetDamage(damage);
    }

    //private GameObject foolPop()
    //{
    //    damageFontFool.
    //}
    //https://ronniej.sfuh.tk/%EC%9C%A0%EB%8B%88%ED%8B%B0%EC%97%90%EC%84%9C-%EC%98%A4%EB%B8%8C%EC%A0%9D%ED%8A%B8-%ED%92%80-object-pool-%EB%A7%8C%EB%93%A4%EA%B8%B0-3-%EC%B4%9D%EC%95%8C-%EB%B0%9C%EC%82%AC%ED%95%98%EA%B8%B0/
}
