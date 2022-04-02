using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    //Debug testing
    public SpawnerControler spawner;
    bool spawned = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDebugSpawn(InputValue value)
	{
        if (value.isPressed && !spawned)
        {
            spawner.SpawnEnemy();
        }
        else spawned = false;
	}
}
