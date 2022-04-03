using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 70.0f;
    public float damage = 5.0f;

    Vector3 dir;
	Transform target;
	GameObject objTarget;

    public void Init(GameObject _target, Transform aim)
	{
		target = aim;
		objTarget = _target;
	}
    // Update is called once per frame
    void Update()
    {
		if(target == null)
		{
			Destroy(gameObject);
			return;
		}

		dir = target.transform.position - transform.position;
		if (dir.magnitude <= speed * Time.deltaTime)
		{
			Hit();
			return;
		}
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }
	
	private void Hit()
	{
		objTarget.GetComponent<MouseControler>().Damage(damage, dir);
		Destroy(gameObject);
		return;
	}
}
