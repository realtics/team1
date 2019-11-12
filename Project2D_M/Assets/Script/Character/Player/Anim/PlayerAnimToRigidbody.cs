using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimToRigidbody : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D = null;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = this.transform.parent.GetComponent<Rigidbody2D>();
    }

    public void PlayerRigidbodyReset()
    {
        m_rigidbody2D.mass = 1.0f;
        m_rigidbody2D.gravityScale = 5.0f;
        m_rigidbody2D.constraints = RigidbodyConstraints2D.None;
        m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

    }
    public void FreezeY()
    {
        m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
    }

    public void FreezeX()
    {
        m_rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
    }
}
