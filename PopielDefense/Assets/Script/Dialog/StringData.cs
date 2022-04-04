using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New String Data", menuName = "Data/String", order = 1)]
public class StringData : ScriptableObject
{
	public int id = 0;
	public int nextId = -1;
	public Sprite image;
	public Sprite backgroundImage;
	[TextArea(25,25)]
	public string data = "";
}
