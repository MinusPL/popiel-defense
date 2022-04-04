using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Data/Enemy", order = 1)]
public class Enemy : ScriptableObject
{
	public int id;
	public GameObject prefab;
}
