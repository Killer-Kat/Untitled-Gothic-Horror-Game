using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCloud : MonoBehaviour //Just a death cloud prefab that gets spawned to play the animation.
{
    public bool IsAnimationDone = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsAnimationDone == true)
        {
            Destroy(gameObject);
            
        }
    }
}
