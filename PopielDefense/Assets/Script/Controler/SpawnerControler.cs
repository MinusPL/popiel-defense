using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControler : MonoBehaviour
{
    [SerializeField]
    GameObject points;
    [SerializeField]
    Registry reg;
    Waypoints path;
    public int spawnerID = 0;

    private bool running = false;
    private Queue<int> enemySpawns;

    private float timeBetweenSpawns = 0f;
    private float BSTimer = 0f;

    private void Awake()
    {
        path = new Waypoints();
        path.points = new Transform[points.transform.childCount];
        for (int i = 0; i < path.points.Length; i++)
        {
            path.points[i] = points.transform.GetChild(i);
        }
        path.CalculateLengths();
        enemySpawns = new Queue<int>();
    }

    //Leave for endless mode, maybe in later patch / DLC
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemySpawns.Count == 0) running = false;

        if(running)
		{
            if(BSTimer <= 0f)
			{
                BSTimer = timeBetweenSpawns;
                int id = enemySpawns.Dequeue();
                Enemy en = reg.GetEnemy(id);
                if(en == null)
				{
                    Debug.LogError($"No enemy with ID: {id}!");
                    return;
                }
                var e = Instantiate(en.prefab);
                e.GetComponent<MouseControler>().Init(path, transform);
            }
            BSTimer -= Time.deltaTime;
		}
    }

    public int SetupWave(string enemies, float _timeBetweenSpawns)
	{
        timeBetweenSpawns = _timeBetweenSpawns;
        enemySpawns.Clear();
        var values = enemies.Trim().Split(',');
        for(int i = 0; i < values.Length; i++)
		{
            enemySpawns.Enqueue(int.Parse(values[i]));
		}
        BSTimer = timeBetweenSpawns;
        running = true;
        return enemySpawns.Count;
	}
}
