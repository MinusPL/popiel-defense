using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControler : MonoBehaviour
{
    public GameObject mouseM;
    [SerializeField]
    GameObject points;
    Waypoints path;

    public float MPS = 1f;
    float time = 1f;
    float timer = 0.0f;

    private void Awake()
    {
        path = new Waypoints();
        path.points = new Transform[points.transform.childCount];
        for (int i = 0; i < path.points.Length; i++)
        {
            path.points[i] = points.transform.GetChild(i);
        }
        path.CalculateLengths();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time =  1f / MPS;
        timer += Time.deltaTime;
		if (timer > time)
		{
			SpawnEnemy();
            timer = 0.0f;
		}
    }

    public void SpawnEnemy()
	{
        GameObject e = null;
        e = Instantiate(mouseM);
        e.GetComponent<MouseControler>().Init(path, transform);
	}
}
