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
            for (int i = 0; i < 100; i++)
                spawner.SpawnEnemy();
        }
        else spawned = false;
    }

    void OnDebugEXPLOSION()
    {
        var l = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < l.Length; i++)
        {
            l[i].transform.position = new Vector3(Random.Range(-100, 100), Random.Range(0, 100), Random.Range(-100, 100));
        }
    }


}
