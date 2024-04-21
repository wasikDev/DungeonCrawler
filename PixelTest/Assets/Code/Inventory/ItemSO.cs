using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = StatToChange.none;
    public int amountToChangeStat;
    public AttributesToChange attributesToChange = AttributesToChange.none;
    public int amountToChangeAttribute;

    public bool UseItem()
    {
        if (statToChange == StatToChange.health)
        {
            HealthManager currentHealthManager = GameObject.Find("Player").GetComponent<HealthManager>();

            if (currentHealthManager == null)
            {
                Debug.LogError("Failed to find HealthManager component.");
                return false;
            }

            Debug.Log("Current health / Max health: " + currentHealthManager.currentHealth + " / " + currentHealthManager.maxHealth);

            if (currentHealthManager.currentHealth == currentHealthManager.maxHealth)
            {
                Debug.Log("Health is already at max. Cannot use item.");
                return false;
            }
            else
            {
                currentHealthManager.ChangeHealth(amountToChangeStat);
                Debug.Log("Used health item. Health increased by " + amountToChangeStat);
                return true;
            }
        }
        return false;
    }



    public enum StatToChange
    {
        none,
        health,
        mana,
        stamina
    }

    public enum AttributesToChange
    {
        none,
        strength,
        defense,
        intelligence,
        agility
    }
}
