using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script contains all the values that you want to change durring gameplay and then save to a file.
public class PlayerStats : MonoBehaviour
{
    [System.NonSerialized] public UIManager UIMan;
    [System.NonSerialized] public PlayerMovement playerMan;
    
    //Money and Inventory
    public int currentMoney;
    //Health and Armor
    public int currentHealth;
    public int maxHealth;
    //Weapons and Equipment
    public int damage = 10; //Not currently used
    //General
    public static PlayerStats Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null) 
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);//This should (hopefully) never happen
            Debug.LogWarning("Player Stats Singleton Duplicate Deleted");
        }
    }
    void Start()
    {
       // EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        UIMan = FindObjectOfType<UIManager>();
        playerMan = FindObjectOfType<PlayerMovement>();
    }
    public void HurtPlayer(int dmg)
    {
        currentHealth -= dmg;
        UIMan.HealthBarUpdate();
    }

    public void HealPlayer(int amt)
    {
        currentHealth += amt;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIMan.HealthBarUpdate();
    }

    public void GetMoney(int amt) //If only it were that easy in real life.
    {
        currentMoney += amt;
        UIMan.coinGUIupdate();
    }
    /*void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armormod = armormod + newItem.armorModifier;
            damagemod = damagemod + newItem.damageModifer;
            speedmod = speedmod + newItem.speedModifer;
        }
        if (oldItem != null)
        {
            armormod = armormod - oldItem.armorModifier;
            damagemod = damagemod - oldItem.damageModifer;
            speedmod = speedmod - oldItem.speedModifer;
        }
        UIMan.armorGUIupdate();
        playerMan.cacheSpeed();
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

       
        currentHealth = data.currentHealth;
        currentExp = data.currentExp;
        currentMoney = data.currentMoney;
        currentHealthPotions = data.currentHealthPotions;
        maxHealth = data.maxHealth;
        playerArmor = data.playerArmor;
        damage = data.damage;
        moveSpeed = data.moveSpeed;
        currentScene = data.currentScene;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        Debug.Log("x" + position.x + "y" + position.y);
    }*/
}
