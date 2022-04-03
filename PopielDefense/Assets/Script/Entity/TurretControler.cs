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

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
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
        GameObject bulletObject = Instantiate(bullet, firePoint.position, firePoint.rotation);
        bulletObject.GetComponent<Bullet>().Init(target.transform);
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

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
	}
}
