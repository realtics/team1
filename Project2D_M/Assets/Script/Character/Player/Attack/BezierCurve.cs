using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve
{
    private List<Vector3> positions;
	private List<List<Vector3>> listPositions = new List<List<Vector3>>();
	private int count = 0;
	private float dTiem = 0;
	private float m_speed;
	public Vector3 lastPosition { get; private set; }

	public void InitBezier(List<Vector3> _positions,float _speed)
	{
		if (listPositions.Count != 0)
			listPositions.Clear();

		dTiem = 0;
		count = 0;

		positions = _positions;
		m_speed = _speed;

		List<Vector3> listTempPositions = new List<Vector3>();
		
		for (int i = 0; i < positions.Count; ++i)
		{
			if (i == positions.Count - 1)
			{
				lastPosition = positions[i];
				listTempPositions.Add(positions[i]);
				listPositions.Add(listTempPositions);
				break;
			}

			listTempPositions.Add(positions[i]);
			if(listTempPositions.Count % 3 == 0)
			{
				Vector3 lerpPos = Vector3.Lerp(positions[i], positions[i + 1], 0.5f);
				listTempPositions.Add(lerpPos);
				listPositions.Add(listTempPositions);
				listTempPositions = new List<Vector3>();
				listTempPositions.Add(lerpPos);
			}
		}
	}

	public Vector3 GetPoint(float _speedPlus)
	{
		Vector3 result = GetPointOnBezierCurve(listPositions[count],dTiem);

		dTiem += Time.deltaTime * m_speed;

		if(dTiem > 1)
		{
			dTiem = 0;
			count++;
			m_speed *= _speedPlus;
			if (listPositions.Count == count)
			{
				result = lastPosition;
			}
		}

		return result;
	}

	public Vector3 GetPointOnBezierCurve(List<Vector3> _positions,float t)
	{
		float u = 1f - t;
		List<Vector3> temp = _positions;
		List<float> time = new List<float>();
		List<float> uTime = new List<float>();
		Vector3 result = Vector3.zero;

		for (int i = 0; i < temp.Count; ++i)
		{
			if (i == 0)
			{
				time.Add(1);
				uTime.Add(1);
			}
			else
			{
				time.Add(time[i - 1] * t);
				uTime.Add(uTime[i - 1] * u);
			}
		}

		for (int i = 0; i < temp.Count; ++i)
		{
			float sum = time[i] * uTime[temp.Count-i - 1];
			if (i != 0 && i != temp.Count - 1)
				sum = sum * (float)(temp.Count - 1);

			result += (temp[i] * sum);
		}

		return result;
	}
}
