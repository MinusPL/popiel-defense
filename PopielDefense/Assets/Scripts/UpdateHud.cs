using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateHud : MonoBehaviour
{
    public int enemyCounter = 23;
    public int hpCounter = 23;
    public int goldCounter = 23;
    public TextMeshProUGUI EnemyCounterHandle;
    public TextMeshProUGUI TowerHpHandle;
    public TextMeshProUGUI GoldCounterHandle;
    // Start is called before the first frame update
    void Start()
    {
        EnemyCounterHandle.text = "" + 0;
        TowerHpHandle.text = "" + 0;
        GoldCounterHandle.text = "" + 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemyCounter();
        UpdateGoldCounter();
        UpdateHealthCounter();
    }

    void UpdateEnemyCounter()
    {
        EnemyCounterHandle.text = "" + enemyCounter;
    }
    
    void UpdateGoldCounter()
    {
        GoldCounterHandle.text = "" + goldCounter;
    }
    
    void UpdateHealthCounter()
    {
        TowerHpHandle.text = "" + hpCounter;
    }
}
