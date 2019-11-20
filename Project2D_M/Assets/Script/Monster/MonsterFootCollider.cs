using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFootCollider : MonoBehaviour
{
    private MonsterAI m_monsterAI = null;

    private void Awake()
    {
        m_monsterAI = this.transform.parent.GetComponent<MonsterAI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            m_monsterAI.SetMonsterPositionGround();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            m_monsterAI.SetMonsterPositionAir();
        }
    }
}
