using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour //This script is on the explosion prefab spawned by bombs (or maybe other things in the future)
{
    public int damage;
    public bool isAnimationDone = false;
    private AudioManager audioMan;
    void Start()
    {
        audioMan = FindObjectOfType<AudioManager>();
        audioMan.Play("Explosion");
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimationDone)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerStats.Instance.HurtPlayer(damage);
        }
        
    }
}
