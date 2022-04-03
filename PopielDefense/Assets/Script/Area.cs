using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public Vector2 size;
    public float meeleLineDistance = 4;
    public float rangedLineDistance = -4;
    public float supportLineDistance = 0;
    public int maxEnemiesInRow = 10;

    List<GameObject> enemies;
    float[] positions;
    bool[] posTaken;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        positions = new float[maxEnemiesInRow];
        posTaken = new bool[maxEnemiesInRow];
        for (int i = 0; i < maxEnemiesInRow; i++)
        {
            positions[i] = (size.x / (maxEnemiesInRow + 1) * (i + 1)) - size.x / 2;
            posTaken[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnDrawGizmos()
	{
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(size.x, 0.0f, size.y));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(-size.x/2, 0, meeleLineDistance), new Vector3(size.x/2, 0, meeleLineDistance));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(-size.x/2, 0, rangedLineDistance), new Vector3(size.x/2, 0, rangedLineDistance));
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(-size.x / 2, 0, supportLineDistance), new Vector3(size.x / 2, 0, supportLineDistance));
	}

	public void OnTriggerEnter(Collider other)
	{
        if(other.CompareTag("Enemy"))
		{
            //think about this
            enemies.Add(other.gameObject);
            float zPos = 0.0f;
            switch (other.gameObject.GetComponent<MouseControler>().type)
            {
                case EnemyType.meele:
                    zPos = meeleLineDistance;
                    break;
                case EnemyType.ranged:
                    zPos = rangedLineDistance;
                    break;
                case EnemyType.support:
                    zPos = supportLineDistance;
                    break;
            }
            float zPosRange = Random.Range(-0.5f, 0.5f);
            if (enemies.Count < maxEnemiesInRow)
            {
                int r;
                while (true)
                {
                    r = Random.Range(0, maxEnemiesInRow);
                    if (posTaken[r]) continue;
                    else break;
                }
                other.gameObject.GetComponent<MouseControler>().SetTarget(transform.TransformPoint(new Vector3(positions[r], 0, zPos+zPosRange)));
            }
			else
			{
                other.gameObject.GetComponent<MouseControler>().SetTarget(transform.TransformPoint(new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, zPos+zPosRange)));
			}
        }
	}
}
