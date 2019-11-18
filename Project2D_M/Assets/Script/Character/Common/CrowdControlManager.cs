using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdControlManager : MonoBehaviour
{
    private CharacterMove m_characterMove = null;
    private CharacterJump m_characterJump = null;

    private void Awake()
    {
        m_characterMove = this.GetComponent<CharacterMove>();
        m_characterJump = this.GetComponent<CharacterJump>();
    }

    public virtual void Stiffen(float _second)
    {
        StartCoroutine(nameof(StiffenCoroutine), _second);
    }

    IEnumerator StiffenCoroutine(float _second)
    {
        m_characterMove.enabled = false;
        m_characterJump.enabled = false;

        yield return new WaitForSeconds(_second);

        m_characterMove.enabled = true;
        m_characterJump.enabled = true;
    }
}
