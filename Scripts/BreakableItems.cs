using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableItems : MonoBehaviour
{
    public GameObject ContainedItem;
    public bool ContainsItem;
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
        if (collision.gameObject.tag == "Attack")
        {
            Break();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack" )
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
