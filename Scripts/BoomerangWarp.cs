using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangWarp : MonoBehaviour //This effect is spawned in to trigger the boomerangs warping return
{
    
    [SerializeField] private AudioManager audioMan;
    private UIManager UIMan;
    public string WarpSound = "WarpSound";
    public bool IsWarpAnmDone = false;
    void Start()
    {
        
        audioMan = FindObjectOfType<AudioManager>();
        UIMan = FindObjectOfType<UIManager>(); //Maybe I should make the UI man a singleton

        audioMan.Play(WarpSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsWarpAnmDone) //The animation in unity flips the bool to true at the last frame
        {
            FinishBoomerangWarp();
        }
    }
    public void FinishBoomerangWarp()
    {
        PlayerStats.Instance.Boomerangs += 1; //Gives the player another boomerang 
        UIMan.BoomerangCounterUpdate();
        Destroy(gameObject);
    }
}
