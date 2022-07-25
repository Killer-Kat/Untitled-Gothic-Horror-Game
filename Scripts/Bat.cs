using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
   
    //Glitch with animation, its not playing the first frame of animation, maybe because sampling is set to 40

    private Animator BatAnimator;
    public Rigidbody2D rb;
    private AudioManager audioMan;
    private PlayerStats pStats;
    public int health = 10;
    public int Damage = 5;
    public float moveSpeed = 3f;
    public string DeathSound = "";
    private float moveTimer = 5f;
    private float moveTime = 3f;
    public int direction;



    void Start()
    {
        audioMan = FindObjectOfType<AudioManager>();
        pStats = FindObjectOfType<PlayerStats>();
        BatAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        moveTimer -= Time.deltaTime;
        moveTime -= Time.deltaTime;
        if(moveTimer <= 0)
        {
                direction = Random.Range(0, 4);
                BatAnimator.SetInteger("direction", direction);
            moveTimer = 5f;
            moveTime = 1.5f;
        }
        if(moveTime <= 0)
        {
            direction = 4;
        }
        switch (direction)
        {
            case 0:
                rb.MovePosition(rb.position + new Vector2(0, 1) * moveSpeed * Time.fixedDeltaTime);  //moves the player object
                break;
            case 1:
                rb.MovePosition(rb.position + new Vector2(1, 0) * moveSpeed * Time.fixedDeltaTime);  //moves the player object
                break;
            case 2:
                rb.MovePosition(rb.position + new Vector2(0, -1) * moveSpeed * Time.fixedDeltaTime);  //moves the player object
                break;
            case 3:
                rb.MovePosition(rb.position + new Vector2(-1, 0) * moveSpeed * Time.fixedDeltaTime);  //moves the player object
                break;
            case 4:
                //Do nothing
                break;

        }
    }
    private void GetHurt()
    {
        health -= 10;
        if (health <= 0)
        {
            Die();
        }
        
    }
    private void Die()
    {
        audioMan.Play(DeathSound);
        Destroy(gameObject);
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
}
