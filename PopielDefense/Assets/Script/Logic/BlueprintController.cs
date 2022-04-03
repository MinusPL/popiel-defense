using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlueprintController : MonoBehaviour
{
    public Material okMaterial;
    public Material badMaterial;
    public GameObject building;

    RaycastHit hit;
    int cCount = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Mouse.current.position.ReadValue());
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out hit, 50000f, 1 << 8))
        {
            transform.position = hit.point;
        }

        SetMaterial(cCount > 0 ? badMaterial : okMaterial);
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Building"))
		{
            cCount++;
		}
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Building"))
        {
            cCount--;
        }
    }

    private void SetMaterial(Material mat)
	{
        Renderer r = gameObject.GetComponent<Renderer>();
        if (r != null) r.material = mat;
        var children = GetComponentsInChildren<Renderer>();
        foreach(var ren in children)
		{
            ren.material = mat;
		}
	}

    public void OnFire(InputValue value)
	{
        if(value.isPressed && cCount == 0)
		{
            Instantiate(building, transform.position, transform.rotation);
            Destroy(gameObject);
            return;
		}
	}
}
