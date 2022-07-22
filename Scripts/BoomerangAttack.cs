using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangAttack : MonoBehaviour
{
    public SpriteRenderer BoomerangSprite;
    public Rigidbody2D rb;
    public float rotationValue;
    public float returnSpeed;
    private float returnSpeedBoost = 0;
    bool IsReturning;

    Vector2 PlayerDist;
    [System.NonSerialized] public PlayerMovement playerMan;

    public float returnTimer = 3;
    // Start is called before the first frame update
    void Start()
    {
        playerMan = FindObjectOfType<PlayerMovement>();
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
           // Debug.Log(returnSpeedBoost);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsReturning = true;
        if (collision.gameObject.tag == "Player")
            Destroy(gameObject);
           
    }
}
