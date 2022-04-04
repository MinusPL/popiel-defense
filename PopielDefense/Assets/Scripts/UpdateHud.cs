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
    public TextMeshProUGUI enemyCounterHandle;
    public TextMeshProUGUI towerHpHandle;
    public TextMeshProUGUI goldCounterHandle;
    // Start is called before the first frame update
    void Start()
    {
        enemyCounterHandle.text = "" + 0;
        towerHpHandle.text = "" + 0;
        goldCounterHandle.text = "" + 0;
        
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
}
