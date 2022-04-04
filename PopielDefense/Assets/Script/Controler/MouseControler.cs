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

    public float attackCooldown = 2.0f;
    private float attackTimer = 2.0f;
    public float attackDamage = 1.0f;
    public float damageDelay = 0.4f;
    private float damageDelayTimer = 0f;
    private bool attacking = false;

    Waypoints path;

    Vector3 target;
    int tIndex = 0;
    GameObject popiel;
    bool insideDestination = false;

    public GameObject canvas;
    public Image healthBar;

    Animator animator;

    public GameObject ragdoll;
    public GameObject bulletTarget;
    private WaveManager waveManager;
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
        animator = GetComponent<Animator>();
        waveManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (insideDestination)
        {
            if (attackTimer >= attackCooldown)
            {
                attackTimer = 0.0f;
                Attack();
            }
            attackTimer += Time.deltaTime;
        }
        
        if(attacking)
		{
            if(damageDelayTimer >= damageDelay)
			{
                damageDelayTimer = 0f;
                attacking = false;
                popiel.GetComponent<TowerManager>().TakeDamage(attackDamage);
			}
            damageDelayTimer += Time.deltaTime;
		}
        
        //UI face camera
        canvas.transform.rotation = Camera.main.transform.rotation;
        //canvas.transform.LookAt(Camera.main.transform);
    }

    private void Attack()
	{
        attacking = true;
        //Set attack to Popiel timer
        animator.SetTrigger("Attack");
	}

    private void Move()
	{
        if (Vector3.Distance(transform.position, target) > 0.1f)
        {
            Vector3 dir = target - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir.normalized), Time.deltaTime * 10.0f);
            animator.SetBool("Moving", true);
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
                animator.SetBool("Moving", false);
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

    public void Damage(float dmg, Vector3 direction = default(Vector3))
    {
        if (currentHealth > 0f)
        { 
            currentHealth -= dmg;
            if (currentHealth <= 0f)
            {
                var go = Instantiate(ragdoll, transform.position, transform.rotation);
                if (direction.magnitude != 0)
                    go.GetComponent<Ragdoll>().SetForce(new Vector3(direction.x, Random.Range(0.1f, 0.2f), direction.z), 40000f);

                waveManager.enemyCount--;
                Destroy(gameObject);
                return;
            }
        }
        healthBar.fillAmount = currentHealth / health;
	}
}
