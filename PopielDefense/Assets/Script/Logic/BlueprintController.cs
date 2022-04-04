using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlueprintController : MonoBehaviour
{
    public Material okMaterial;
    public Material badMaterial;
    public GameObject building;
    public int price = 200;
    private BuildingManager bManager;
    private ResourceManager rManager;
    RaycastHit hit;
    int cCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        bManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BuildingManager>();
        rManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ResourceManager>();
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
        if(cCount > 0 || rManager.GetMoney() < price)
		{
            SetMaterial(badMaterial);
		}
		else
		{
            SetMaterial(okMaterial);
        }
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
        if(value.isPressed && cCount == 0 && rManager.GetMoney() >= price)
		{
            Instantiate(building, transform.position, transform.rotation);
            rManager.SubtractMoney(price);
            bManager.Unlock();
            Destroy(gameObject);
            return;
		}
	}

    public void OnCancelBuild(InputValue value)
	{
        if (value.isPressed)
        {
            bManager.Unlock();
            Destroy(gameObject);
            return;
        }
    }
}
