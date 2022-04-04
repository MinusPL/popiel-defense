using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Registry : MonoBehaviour
{
    private List<Enemy> enemyRegistry;
    private List<Wave> waveRegistry;

    void Start()
    {
        enemyRegistry = new List<Enemy>();
        waveRegistry = new List<Wave>();

        {
            var loaded = Resources.LoadAll("Definitions/Enemy", typeof(Enemy));
            foreach (var e in loaded)
            {
                enemyRegistry.Add((Enemy)e);
            }
        }
        {
            var loaded = Resources.LoadAll("Definitions/Wave", typeof(Wave));
            foreach (var e in loaded)
            {
                waveRegistry.Add((Wave)e);
            }
        }
    }

    public Enemy GetEnemy(int id)
    {
        return enemyRegistry.FirstOrDefault<Enemy>(s => s.id == id);
    }

    public Wave GetWave(int waveIndex, int spawnerID)
    {
        return waveRegistry.FirstOrDefault<Wave>(s => s.waveNumber == waveIndex && s.spawnerID == spawnerID);
    }
}
