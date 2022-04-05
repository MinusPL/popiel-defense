using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public float maxHP = 100.0f;
    public float healAmount = 25.0f;
    public int healPrice = 200;

    private float currentHP;

    private LevelUIManager uiManager;
    private ResourceManager rManager;

    public UpdateHud hud;
    void Start()
    {
        currentHP = maxHP;
        uiManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelUIManager>();
        rManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ResourceManager>();
    }

	private void Update()
	{
        hud.hpCounter = (int)currentHP;
	
        if(transform.position.y < -100.0f)
		{
            uiManager.GameOver();
            Destroy(gameObject);
            return;
		}

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
            GetComponent<Rigidbody>().isKinematic = false;
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

    public void Heal()
	{
        if(rManager.GetMoney() >= healPrice && currentHP < maxHP)
		{
            rManager.SubtractMoney(healPrice);
            currentHP = currentHP + healAmount > maxHP ? maxHP : currentHP + healAmount; 
		}
	}
}
