using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class UpdateHud : MonoBehaviour
{
    public int enemyCounter;
    public int hpCounter;
    public int goldCounter;
    public int repairCost;
    public int waveNo;
    public int timerSeconds;
    public TextMeshProUGUI enemyCounterHandle;
    public TextMeshProUGUI towerHpHandle;
    public TextMeshProUGUI goldCounterHandle;
    public TextMeshProUGUI repairCostHandle;
    public TextMeshProUGUI waveNoHandle;
    public TextMeshProUGUI timerHandle;

    public GameObject shopHandle;


    private Keyboard kbHandle;

    private BuildingManager bManager;

    public GameObject menu;
    
    // Start is called before the first frame update
    void Start()
    {
        waveNo = 0;
        timerSeconds = 0;
        enemyCounter = 0;
        hpCounter = 0;
        goldCounter = 0;
        //repairCost = 0;
        
        kbHandle = Keyboard.current;
        bManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BuildingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemyCounter();
        UpdateGoldCounter();
        UpdateHealthCounter();
        UpdateRepairCost();
        UpdateWave();
        UpdateTimer();
        if (kbHandle.zKey.wasReleasedThisFrame)
        {
            shopHandle.SetActive(!shopHandle.activeSelf);
        }
    }

    void UpdateTimer()
    {
        timerHandle.text = timerSeconds.ToString();
    }

    void UpdateWave()
    {
        waveNoHandle.text = "" + waveNo;
    }
    
    void UpdateRepairCost()
    {
        repairCostHandle.text = "" + repairCost;
    }
    void UpdateEnemyCounter()
    {
        enemyCounterHandle.text = "" + enemyCounter;
    }
    
    void UpdateGoldCounter()
    {
        goldCounterHandle.text = "" + goldCounter;
    }
    
    void UpdateHealthCounter()
    {
        towerHpHandle.text = "" + hpCounter;
    }

    public void OnCancelBuild(InputValue value)
	{
        if(!bManager.isPlacing)
		{
            menu.SetActive(!menu.activeSelf);
		}
	}

    public void ExitToMenu()
	{
        SceneManager.LoadScene(0);
	}

    public void ReloadLevel()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
