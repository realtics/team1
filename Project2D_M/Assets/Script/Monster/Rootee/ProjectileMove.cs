using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rootee : MonoBehaviour
{
	private BezierCurve m_bezierCurve = null;
	private Vector3 m_position;
	public float speed = 1;

	public void InitProjectile(Vector3 _startPos, float _distance, int _pointCount, float _height,
		bool _up, bool _left, int _num)
	{
		m_bezierCurve = m_bezierCurve ?? new BezierCurve();
		this.transform.position = _startPos;

		if (!_left)
			_distance *= -1;

		m_position = new Vector3(_startPos.x + _distance, _startPos.y, _startPos.z);
		//for(int i = 0)

	}

}
