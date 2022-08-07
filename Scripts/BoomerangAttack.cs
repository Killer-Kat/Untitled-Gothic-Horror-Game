using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Killer-Kat 
public class BoomerangAttack : MonoBehaviour
{
    public SpriteRenderer BoomerangSprite;
    public Rigidbody2D rb;
    public float rotationValue = -1.5f;
    public float returnSpeed;
    private float returnSpeedBoost = 0;
    bool IsReturning;

    Vector2 PlayerDist;
    [System.NonSerialized] public PlayerMovement playerMan;
    private AudioManager audioMan;

    public float returnTimer = 2; //How long before the boomerang returns
    // Start is called before the first frame update
    void Start()
    {
        playerMan = FindObjectOfType<PlayerMovement>();
        audioMan = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.rotation = rb.rotation + rotationValue;
        PlayerDist.x = playerMan.gameObject.transform.position.x - gameObject.transform.position.x;
        PlayerDist.y = playerMan.gameObject.transform.position.y - gameObject.transform.position.y;
    }

    private void FixedUpdate()
    {
        returnTimer -= Time.deltaTime;
        if (returnTimer <= 0)
            IsReturning = true;
        if (IsReturning)
        {
            rb.MovePosition(rb.position + PlayerDist * (returnSpeed + returnSpeedBoost) * Time.fixedDeltaTime);
            returnSpeedBoost += Time.deltaTime / 2;
            //Debug.Log(returnSpeedBoost);
            if(returnSpeedBoost >= 3) //So your boomerang cant get stuck in walls, I should add a sound effect to let the player know that this has  happend
            {
                playerMan.Boomerangs += 1;
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsReturning = true;
        if (collision.gameObject.tag == "Player")
        {
            playerMan.Boomerangs += 1;
            Destroy(gameObject);
        }
           
    }
}
