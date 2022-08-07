using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite CopperCoin;
    public Sprite SilverCoin;
    public Sprite GoldCoin;
    public int value;
    public int CoinType;

    private PlayerStats pStats;

    void Start()
    {
        if(value < 6)
        {
            CoinType = 0;
        }
        else if (value < 50)
        {
            CoinType = 1;
        } else
        {
            CoinType = 2;
        }
        pStats = FindObjectOfType<PlayerStats>();
        switch (CoinType)
        {
            case 0:
                spriteRenderer.sprite = CopperCoin;
                break;     
            case 1:
                spriteRenderer.sprite = SilverCoin;
                break;       
            case 2:
                spriteRenderer.sprite = GoldCoin;
                break;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pStats.GetMoney(value);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
