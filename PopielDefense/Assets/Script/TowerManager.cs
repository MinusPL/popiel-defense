using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public float maxHP = 100.0f;

    private float currentHP;

    public UpdateHud hud;
    void Start()
    {
        currentHP = maxHP;
    }

	private void Update()
	{
        hud.hpCounter = (int)currentHP;
	}

	public float GetCurrentHP()
    {
        return currentHP;
    }

    public void TakeDamage(float amount)
    {
        if(amount >= currentHP)
        {
            currentHP = 0.0f;

            //Ubij wie¿e
        }
        else
        {
            currentHP -= amount;
        }
    }

    public void Repair(float amount)
    {
        if(currentHP + amount > maxHP)
        {
            currentHP = maxHP;
        }
        else
        {
            currentHP += amount;
        }
    }
}
