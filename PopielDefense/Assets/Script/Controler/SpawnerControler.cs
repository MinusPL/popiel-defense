using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControler : MonoBehaviour
{
    public GameObject mouseM;
    public GameObject mouseR;
    public GameObject mouseS;
    [SerializeField]
    GameObject points;
    Waypoints path;

    float time = 3.0f;
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
        timer += Time.deltaTime;
        if(timer > time)
		{
            SpawnEnemy();
            timer = 0.0f;
		}
    }

    public void SpawnEnemy()
	{
        int r = Random.Range(0, 3);
        GameObject e = null;
        switch(r)
		{
            case (int)EnemyType.meele:
                e = Instantiate(mouseM);
                break;
            case (int)EnemyType.ranged:
                e = Instantiate(mouseR);
                break;
            case (int)EnemyType.support:
                e = Instantiate(mouseS);
                break;
        }
        e.GetComponent<MouseControler>().Init(path, transform);
	}
}
