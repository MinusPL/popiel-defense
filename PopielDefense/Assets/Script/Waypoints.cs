using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints
{
	public Transform[] points;
	public float[] segmentsLength;

	public void CalculateLengths()
	{
		segmentsLength = new float[points.Length - 1];
		for(int i = 0; i < segmentsLength.Length; i++)
		{
			segmentsLength[i] = Vector3.Distance(points[i].position, points[i + 1].position);
			if (i > 0) segmentsLength[i] += segmentsLength[i - 1];
		}
	}
}
