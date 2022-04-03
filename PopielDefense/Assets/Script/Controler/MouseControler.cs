using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EnemyType
{
    meele,
    ranged,
    support
}

public class MouseControler : MonoBehaviour
{
    public float speed = 5.0f;
    public float range = 5.0f;
    public EnemyType type;

    public float health = 25f;

    private float currentHealth;

    Waypoints path;

    Vector3 target;
    int tIndex = 0;
    GameObject popiel;
    bool insideDestination = false;

    public GameObject canvas;
    public Image healthBar;

    public void Init(Waypoints p, Transform pos)
	{
        path = p;
        target = path.points[0].position;
        transform.position = pos.position;
	}

	// Start is called before the first frame update
	void Start()
    {
        popiel = GameObject.FindGameObjectWithTag("Popiel");
        if (popiel == null)
		{
            Debug.LogError("Popiel is not here");
            Destroy(gameObject);
		}
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        //UI face camera
        canvas.transform.rotation = Camera.main.transform.rotation;
        //canvas.transform.LookAt(Camera.main.transform);
    }

    private void Move()
	{
        if (Vector3.Distance(transform.position, target) > 0.1f)
        {
            Vector3 dir = target - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }
        else
		{
            if (!insideDestination)
            {
                target = ++tIndex < path.points.Length ? path.points[tIndex].position : transform.position;
            }
            else
            {
                target = transform.position;
            }
        }
    }

    public void SetTarget(Vector3 t)
	{
        target = t;
        insideDestination = true;
	}

    public float GetDistanceToTower()
    {
        float distance = Mathf.Infinity;
        if(tIndex == 0)
		{
            distance = path.segmentsLength[path.segmentsLength.Length - 1] - Vector3.Distance(transform.position, target);
		}
		else
        {
            distance = path.segmentsLength[path.points.Length - tIndex - 1] - Vector3.Distance(transform.position, target);
        }
        return distance; 
    }

    public void Damage(float dmg)
	{
        currentHealth -= dmg;
        if(currentHealth <= 0f)
		{
            //Finished, destroy itself
            Destroy(gameObject);
            return;
		}
        healthBar.fillAmount = currentHealth / health;
	}
}
