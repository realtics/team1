using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMove : MonoBehaviour
{
	private BezierCurve m_bezierCurve = null;
	private Vector3[] postions;
	public int pointCount = 8;
	public int distance = 10;
	public float speed = 1;
	private void Awake()
	{
		m_bezierCurve = new BezierCurve();
		InitFireBall(this.transform.position, 0, 0);
	}

	private void InitFireBall(Vector3 _position, float _distance, int _pointCount)
	{
		_distance = distance;
		_pointCount = pointCount;

		postions = new Vector3[_pointCount];

		postions[_pointCount - 1] = new Vector3(_position.x + _distance, _position.y, _position.z);

		for (int i = _pointCount - 2; i >= 0; --i)
		{
			if (i == 0)
			{
				postions[i] = _position;
				break;
			}

			postions[i] = Vector3.Lerp(_position, postions[i + 1], 0.5f);
		}

		int tempMinus = Random.Range(0, 2);
		int num = 1;
		if (tempMinus == 0)
			tempMinus = -1;
		else tempMinus = 1;

		for (int i = _pointCount - 2; i > 0; --i)
		{
			num *= tempMinus;
			postions[i].y += (_pointCount - i) * num;
		}

		m_bezierCurve.InitBezier(new List<Vector3>(postions), speed);
	}

	private void FixedUpdate()
	{
		if (this.transform.position != m_bezierCurve.lastPosition)
			this.transform.position = m_bezierCurve.GetPoint();
	}
}
