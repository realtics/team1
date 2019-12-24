using System;
using System.Threading.Tasks;
using UnityEngine;

public class QuestObjentMove : MonoBehaviour 
{
	int m_iDirection = 1;

	void Update()
	{
		if (transform.localPosition.x < -1.0f)
		{
            m_iDirection = -1;
		}
		else if (transform.localPosition.x > 1.0f)
		{
            m_iDirection = 1;
		}

		transform.Translate(Vector3.left * 0.1f * Time.deltaTime * m_iDirection);
	}

}
