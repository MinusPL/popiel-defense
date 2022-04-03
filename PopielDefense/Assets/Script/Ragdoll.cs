using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public GameObject rootNode;
    float existTime = 5.0f;
    float existTimer = 0.0f;
    
    public void SetForce(Vector3 direction, float force)
	{
        rootNode.GetComponent<Rigidbody>().AddForce(direction * force);
	}


	private void Update()
	{
		if (existTimer >= existTime) Destroy(gameObject);
		existTimer += Time.deltaTime;
	}

}
