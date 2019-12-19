using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMove : MonoBehaviour
{
	private BezierCurve m_bezierCurve = null;
	private Vector3[] postions;
	public float speed = 1;

	//private void OnDrawGizmos()
	//{
	//	if (postions.Length > 0)
	//	{
	//		for (int i = 0; i < postions.Length; ++i)
	//		{
	//			Gizmos.DrawWireSphere(postions[i], 0.1f);
	//		}
	//	}
	//}

	public void InitFireBall(Vector3 startPos, float _distance, int _pointCount, float _height, bool _up, bool left, int _num)
	{
		m_bezierCurve = m_bezierCurve ?? new BezierCurve();
		postions = new Vector3[_pointCount];
		this.transform.position = startPos;

		if (!left)
			_distance *= -1;

		postions[_pointCount - 1] = new Vector3(startPos.x + _distance, startPos.y, startPos.z);

		for (int i = _pointCount - 2; i >= 0; --i)
		{
			if (i == 0)
			{
				postions[i] = startPos;
				break;
			}
			float tempvalue = Random.Range(0.1f, 0.7f);

			postions[i] = Vector3.Lerp(startPos, postions[i + 1], tempvalue);
		}


		float plusvalue = Random.Range(1.0f, 2.0f);

		int minusNum;

		if (_up)
			minusNum = 1;
		else minusNum = -1;

		for (int i = _pointCount - 2; i > 0; --i)
		{
			minusNum = -1 * minusNum;

			if (i == 1)
				postions[i].y += (_pointCount - i) * minusNum * _num * plusvalue * _height;
			else postions[i].y += minusNum * _num * plusvalue * _height;
		}

		m_bezierCurve.InitBezier(new List<Vector3>(postions), speed);
	}

	private void FixedUpdate()
	{
		if (this.transform.position != m_bezierCurve.lastPosition)
			this.transform.position = m_bezierCurve.GetPoint(3f);
		else
		{
			ObjectPool.Inst.PushToPool(this.gameObject);
		}
	}
}
