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
    public int currentHealthPotions;
    public int currentHealth;
    public int maxHealth;
    public int playerArmor;
    //Weapons and Equipment
    public int damage = 10;
    //General
    public float moveSpeed = 5f;
    public int currentScene;
    //Stat modifiers
    public int armormod = 0;
    public int damagemod = 0;
    public float speedmod = 0;
    
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
        currentHealth -= amt;
        UIMan.HealthBarUpdate();
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
