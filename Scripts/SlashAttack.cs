using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttack : MonoBehaviour
{
    //Spawns in as a hitbox for player slash attacks.
    //Because weapon sprite is rendered from player sprite
    //Because I had difficulty with laying the sprites
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //idk
        }
    }
}
