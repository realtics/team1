using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBezierCurve : MonoBehaviour
{
    private List<Vector3> positions;
	public List<GameObject> gameObjects;

	private List<List<Vector3>> listPositions = new List<List<Vector3>>();
	int count = 0;
	float dTiem = 0;
	private void Start()
	{
		positions = new List<Vector3>();
		positions.Add(this.transform.position);

		for (int i = 0; i < gameObjects.Count; ++i)
		{
			positions.Add(gameObjects[i].transform.position);
		}

		List<Vector3> temp = new List<Vector3>();
		
		for (int i = 0; i < positions.Count; ++i)
		{
			temp.Add(positions[i]);
			if(temp.Count % 4 == 0)
			{
				listPositions.Add(temp);
				temp = new List<Vector3>();
				temp.Add(positions[i]);
			}

			if(i == positions.Count-1)
				listPositions.Add(temp);
		}
	}
	private void Update()
	{
		this.transform.position = GetPointOnBezierCurve(listPositions[count], dTiem);
		dTiem += Time.deltaTime * 0.8f;

		if (dTiem > 1)
		{
			dTiem = 0;
			count++;
		}
	}

	Vector3 GetPointOnBezierCurve(List<Vector3> _positions, float t)
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
