using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    private Animator SpiderAnimator;
    private SpriteRenderer SpiderSprite;
    public Rigidbody2D rb;
    [SerializeField] private GameObject deathCloud;
    private AudioManager audioMan;
    private PlayerStats pStats;
    public int health = 25;
    public int Damage = 5;
    public float moveSpeed = 3f;
    public string DeathSound = "";
    public string HurtSound = "";

    private Vector3 startingPosition;
    private Transform target;
    private Vector3 roamPosition;
    private float RoamDistCheck = 1f;
    [SerializeField] private int AIState = 0;
    [SerializeField] private float debugReturn;
    void Start()
    {
        audioMan = FindObjectOfType<AudioManager>();
        pStats = FindObjectOfType<PlayerStats>();
        SpiderAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        SpiderSprite = GetComponent<SpriteRenderer>();

        startingPosition = transform.position; //cache starting position
        target = FindObjectOfType<PlayerMovement>().transform;

    }

    // Update is called once per frame
    void Update()
    {
        switch (AIState)
        {
            case 0: //idle
                SpiderAnimator.SetBool("IsMoving", false);
                if(UnityEngine.Random.Range(0, 20000) < 2)
                {
                    Roam();
                }
                break;

            case 1: //Follow player
                FollowPlayer();
                break;

            case 2: //return home
                transform.position = Vector3.MoveTowards(transform.position, startingPosition, moveSpeed * Time.deltaTime);
                SpiderAnimator.SetFloat("Horizontal", (startingPosition.x - transform.position.x));
                SpiderAnimator.SetFloat("Vertical", (startingPosition.y - transform.position.y));
                if(Vector3.Distance(transform.position, startingPosition) < 1f)
                {
                    AIState = 0;
                }
                break;
            case 3: //roam
                transform.position = Vector3.MoveTowards(transform.position, roamPosition, moveSpeed * Time.deltaTime);
                SpiderAnimator.SetFloat("Horizontal", (roamPosition.x - transform.position.x));
                SpiderAnimator.SetFloat("Vertical", (roamPosition.y - transform.position.y));
                debugReturn += Time.deltaTime; //Incase the spider gets stuck on something when this timer runs out it will return to its starting position
                if((Vector3.Distance(transform.position, roamPosition) < RoamDistCheck) || (debugReturn > 7))
                {
                    //reached roam position
                    StartCoroutine(IdleTime());
                    
                }
                break;
        }
    }
    IEnumerator IdleTime()
    {
        AIState = 0;
        yield return new WaitForSeconds(UnityEngine.Random.Range(3, 12));
        AIState = 2;
        SpiderAnimator.SetBool("IsMoving", true);
    }
    private void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        SpiderAnimator.SetBool("IsMoving", true);
        SpiderAnimator.SetFloat("Horizontal", (target.transform.position.x - transform.position.x));
        SpiderAnimator.SetFloat("Vertical", (target.transform.position.y - transform.position.y));
    }
    
    private void Roam()
    {
        SpiderAnimator.SetBool("IsMoving", true);
        roamPosition = GetRoamingPosition();
        debugReturn = 0f;
        AIState = 3;

    }
    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDir() * Random.Range(5f, 20f); //may need to adjust range due to game scale
    }

    private static Vector3 GetRandomDir() //www.youtube.com/watch?v=db0KWYaWfeM
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Attack")
        {
            GetHurt(10);
        }
        else if (collision.gameObject.tag == "Explosion")
        {
            GetHurt(collision.gameObject.GetComponent<Explosion>().damage);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pStats.HurtPlayer(Damage);
        }
        if (collision.gameObject.tag == "Attack")
        {
            GetHurt(10);
        }
        
    }

    public void SpiderSense() //My Spider Sense is Tingling!
    {
        AIState = 1;
        SpiderAnimator.SetBool("IsMoving", true);
    }

    private void GetHurt(int dmg)
    {
        StartCoroutine(FlashRed());
        health -= dmg;
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
        SpiderSprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        SpiderSprite.color = Color.white;

    }
}
