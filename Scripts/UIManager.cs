using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI healthPotionText;
    public TextMeshProUGUI armorText;
    private GameObject[] GUIs;

    //This is so that I dont generate multiple GUI's 
    //Prob not needed anymore, this was from my old game
    private void OnLevelWasLoaded(int level)
    {
        GUIs = GameObject.FindGameObjectsWithTag("MainGUI");

        if (GUIs.Length > 1)
        {
            Destroy(GUIs[1]);
        }
    }

    void Start()
    {
        Invoke("HealthBarUpdate", 1);
        
        DontDestroyOnLoad(gameObject);
    }
    public void UpdateAll()
    {
        HealthBarUpdate();

        coinGUIupdate();
    }
   
    public void HealthBarUpdate()
    {
        healthBar.maxValue = PlayerStats.Instance.maxHealth;
        healthBar.value = PlayerStats.Instance.currentHealth;
        hpText.text = "HP: " + PlayerStats.Instance.currentHealth + "/" + PlayerStats.Instance.maxHealth;
    }
   
    public void coinGUIupdate()
    {
    coinText.text = "" + PlayerStats.Instance.currentMoney;
    }

    
}
