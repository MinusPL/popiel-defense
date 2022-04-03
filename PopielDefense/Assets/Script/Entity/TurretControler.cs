using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControler : MonoBehaviour
{
    public float range = 15.0f;
    public GameObject head;
    public float rotationSpeed = 10.0f;
    public float fireRate = 1f;
    private float fireTimer = 0;
    GameObject target = null;

    public GameObject bullet;
    public Transform firePoint;

    Vector3 dir2;

    private float targetSeekTime = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, targetSeekTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            dir2 = dir;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            Vector3 rot = Quaternion.Lerp(head.transform.rotation, lookRot, Time.deltaTime * rotationSpeed).eulerAngles;
            head.transform.rotation = Quaternion.Euler(0f, rot.y, 0f);
            if (fireTimer <= 0f)
            { 
                Fire();
            }
        }

        if(fireTimer <= 0f)
		{
            fireTimer = 1f / fireRate;
		}
        fireTimer -= Time.deltaTime;
    }

    private void Fire()
	{
        GameObject bulletObject = Instantiate(bullet, firePoint.position, Quaternion.LookRotation(dir2));
        bulletObject.GetComponent<Bullet>().Init(target, target.GetComponent<MouseControler>().bulletTarget.transform);
    }

    private void UpdateTarget()
	{
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        float distanceToTurret = Mathf.Infinity;
        foreach(var enemy in enemies)
		{
            float distance = enemy.GetComponent<MouseControler>().GetDistanceToTower();
            distanceToTurret = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance && distanceToTurret <= range)
			{
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
		}
        target = nearestEnemy;
	}

    public void ChangeSeekTime(float time)
	{
        CancelInvoke("UpdateTarget");
        targetSeekTime = time;
        InvokeRepeating("UpdateTarget", targetSeekTime, targetSeekTime);
	}

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + dir2.normalized * 10.0f);
    }
}
