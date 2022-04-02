using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MouseControler : MonoBehaviour
{
    public float speed = 5.0f;
    public float range = 5.0f;

    Waypoints path;

    Transform target;
    int tIndex = 0;
    GameObject popiel;
    NavMeshAgent navAgent;

    public void Init(Waypoints p, Transform pos)
	{
        path = p;
        target = path.points[0];
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
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.SetDestination(popiel.transform.position);
        navAgent.isStopped = true;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
	{
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            if (Vector3.Distance(transform.position, target.position) <= 0.1f)
            {
                target = ++tIndex < path.points.Length ? path.points[tIndex] : null;
            }
            transform.Translate(dir.normalized * speed * Time.deltaTime);
            if (target == null)
            {
                navAgent.isStopped = false;
                Debug.Log("Agent enabled");
                
            }
        }
        else
		{
            //navAgent.SetDestination(popiel.transform.position);
        }
    }
}
