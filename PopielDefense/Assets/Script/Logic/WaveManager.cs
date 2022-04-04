using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public float timeBetweenWaves = 60.0f;
    public int waves = 10;
    private float BWTimer = 0.0f;

    private int currentWave = 1;
    [SerializeField]
    private bool isRunning = false;
    private bool shouldRun = true;
    private bool waveInProgress = false;

    public GameObject[] spawners;
    public Registry reg;
    public UpdateHud hud;

    public int enemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        BWTimer = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRunning && shouldRun && !waveInProgress)
		{
            if(BWTimer <= 0f)
			{
                BWTimer = timeBetweenWaves;
                waveInProgress = true;
                StartWave(currentWave);
			}
            BWTimer -= Time.deltaTime;
            hud.timerSeconds = (int)BWTimer + 1;
		}

        if(waveInProgress)
		{
            hud.timerSeconds = 0;
            if (enemyCount <= 0)
			{
                currentWave++;
                if(currentWave > waves+1)
				{
                    shouldRun = false;
				}
                waveInProgress = false;
			}
		}
        hud.enemyCounter = enemyCount;
        hud.waveNo = currentWave;
    }

    private void StartWave(int waveIndex)
	{
        Debug.Log($"Starting wave {waveIndex}");
        foreach (var s in spawners)
        {
            var wave = reg.GetWave(waveIndex, s.GetComponent<SpawnerControler>().spawnerID);
            if (wave == null) continue; //No wave definition for us this round
            else
            {
                enemyCount += s.GetComponent<SpawnerControler>().SetupWave(wave.enemies, wave.spawnTime); //hell yeah! We have some data!
            }
        }
    }
}
