using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 70.0f;
    public float damage = 5.0f;

    private Transform target;
    
    public void Init(Transform _target)
	{
        target = _target;
	}
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        
        if(dir.magnitude <= speed*Time.deltaTime)
		{
            Hit();
            return;
		}

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }

	private void Hit()
	{
        target.gameObject.GetComponent<MouseControler>().Damage(damage);
        Destroy(gameObject);
        return;
        //Destroy(target.gameObject);
    }
}
