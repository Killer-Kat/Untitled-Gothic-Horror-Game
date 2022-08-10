using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableItems : MonoBehaviour
{
    public GameObject ContainedItem;
    public bool ContainsItem;
    public bool isBombOnly; //Can the item only be broken by bombs?
    public string BreakSound;
    private AudioManager audioMan;

    void Start()
    {
        audioMan = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Attack" && isBombOnly == false)
        {
            Break();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack" && isBombOnly == false)
        {
            Break();
        }
        else if (collision.gameObject.tag == "Explosion")
        {
            Break();
        }
    }
    public void Break()
    {
        audioMan.Play(BreakSound);
        if (ContainsItem)
        {
            Instantiate(ContainedItem, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
