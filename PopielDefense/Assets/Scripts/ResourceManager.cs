using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private uint money;

    public void AddMoney(uint amount)
    {
        money += amount;
    }

    public void SubtractMonet(uint amount)
    {
        if(amount > money)
        {
            money = 0;
        }
        else
        {
            money -= amount;
        }
    }

    public uint GetMoney()
    {
        return money;
    }

    public void SetMoney(uint amount)
    {
        money = amount;
    }
}
