using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControler : MonoBehaviour
{
    public GameObject mouse;
    [SerializeField]
    GameObject points;
    Waypoints path;


    private void Awake()
    {
        path = new Waypoints();
        path.points = new Transform[points.transform.childCount];
        for (int i = 0; i < path.points.Length; i++)
        {
            path.points[i] = points.transform.GetChild(i);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEnemy()
	{
        var e = Instantiate(mouse);
        e.GetComponent<MouseControler>().Init(path, transform);
	}
}
