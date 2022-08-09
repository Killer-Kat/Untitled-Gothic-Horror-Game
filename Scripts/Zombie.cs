using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Animator ZombieAnimator;
    private SpriteRenderer ZombieSprite;
    public Rigidbody2D rb;
    [SerializeField] private GameObject deathCloud;
    private AudioManager audioMan;
    private PlayerStats pStats;
    public int health = 25;
    public int Damage = 5;
    public float moveSpeed = 3f;
    public string DeathSound = "";
    public string HurtSound = "";
    // Start is called before the first frame update
    void Start()
    {
        audioMan = FindObjectOfType<AudioManager>();
        pStats = FindObjectOfType<PlayerStats>();
        ZombieAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ZombieSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pStats.HurtPlayer(Damage);
        }
        if (collision.gameObject.tag == "Attack")
        {
            GetHurt();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Attack")
        {
            GetHurt();
           
        }
    }
    private void GetHurt()
    {
        StartCoroutine(FlashRed());
        health -= 10;
        audioMan.Play(HurtSound);
        if (health <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        audioMan.Play(DeathSound);
        Instantiate(deathCloud, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public IEnumerator FlashRed() //www.youtube.com/watch?v=veFcxTNsfZY
    {
        ZombieSprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        ZombieSprite.color = Color.white;

    }
}
