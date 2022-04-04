using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Data/Wave", order = 2)]
public class Wave : ScriptableObject
{
	public int waveNumber;
	public int spawnerID;
	public string enemies;
	[Header("How much time is supposed to pass between enemy spawns\nin given wave")]
	public float spawnTime;
}
