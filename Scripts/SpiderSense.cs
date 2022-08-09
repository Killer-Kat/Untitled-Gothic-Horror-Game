using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script is so the spider can have a seprate hitbox for its player detection radius, otherwise the detection radius will count as part of its hitbox for damage reasons
public class SpiderSense : MonoBehaviour
{
    public Spider SpiderMain;
    void Start()
    {
        SpiderMain = GetComponentInParent<Spider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            SpiderMain.SpiderSense(); //My Spider Sense is Tingling!

        }
    }
}
