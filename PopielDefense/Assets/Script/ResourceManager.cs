using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private int money;

    public UpdateHud hud;
    public int startingMoney = 200;

	private void Start()
	{
        money = startingMoney;
        Invoke("SlowStart", 0.1f);
	}

	public void AddMoney(int amount)
    {
        money += amount;
        hud.goldCounter = money;
    }

    public void SubtractMoney(int amount)
    {
        if (amount > money)
        {
            money = 0;
        }
        else
        {
            money -= amount;
        }
        hud.goldCounter = money;
    }

    public int GetMoney()
    {
        return money;
    }

    public void SetMoney(int amount)
    {
        money = amount;
        hud.goldCounter = money;
    }

    private void SlowStart()
	{
        hud.goldCounter = money;
    }
}