using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.InputSystem;
//using System;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator playerAnimator;
   // public Animator weaponAnimator; // Was trying to figure out how to use LPC weapon sprites but its on hold for now
    //public Animator TorsoAnimator;
    public SpriteRenderer playerSpriteRenderer;

    private float attackTime = 0.9f;
    private float attackCounter = 1f;
    [SerializeField] private bool isAttacking;
    public GameObject Boomerang;
    public float BoomerangThrowSpeed;
    public int Boomerangs = 1;

    [SerializeField] private InputActions movementAction;
    [SerializeField] private InputAction movement;
    public Vector2 lastMove;
    public float moveSpeed = 5;

    [SerializeField] private PauseMenu pMenu;
    private AudioManager audioMan;
    private int soundNum;

    [SerializeField] private GameObject SlashUp;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        audioMan = FindObjectOfType<AudioManager>();
        pMenu = FindObjectOfType<PauseMenu>(true); //Dont forget to enable the Pause menu container gameobject, only the panel should ever be disabled.
    }
    private void Awake()
    {
        movementAction = new InputActions();
    }
    void OnEnable()
    {

       movement = movementAction.Player.Movement;
       movement.Enable();

       movementAction.Player.Attack.performed += Attack;
       movementAction.Player.Attack.Enable();

       movementAction.Player.ThrowBooomerang.performed += ThrowBooomerang;
       movementAction.Player.ThrowBooomerang.Enable();

       movementAction.Player.PauseGame.performed += PauseGame;
       movementAction.Player.PauseGame.Enable();

        /*movementAction.Player.DrinkHealthPotion.performed += drinkHealthPotionCheck;
       movementAction.Player.DrinkHealthPotion.Enable();

       movementAction.Player.PauseGame.performed += pingPauseMenu;
       movementAction.Player.PauseGame.Enable();

       movementAction.Player.OpenInventory.performed += pingIntventoryUI;
       movementAction.Player.OpenInventory.Enable(); */
    }
    private void OnDisable()
    {
        movement.Disable();
    }
    void FixedUpdate()
    {
        if(isAttacking == false) { 
        rb.MovePosition(rb.position + movement.ReadValue<Vector2>() * moveSpeed * Time.fixedDeltaTime);  //moves the player object
        }
    }

    private void Update()
    {
        //Input

        playerAnimator.SetFloat("Horizontal", movement.ReadValue<Vector2>().x);
        playerAnimator.SetFloat("Vertical", movement.ReadValue<Vector2>().y);
        playerAnimator.SetFloat("Speed", movement.ReadValue<Vector2>().sqrMagnitude);
       // TorsoAnimator.SetFloat("Horizontal", movement.ReadValue<Vector2>().x);
       // TorsoAnimator.SetFloat("Vertical", movement.ReadValue<Vector2>().y);
       // TorsoAnimator.SetFloat("Speed", movement.ReadValue<Vector2>().sqrMagnitude);

        if (movement.ReadValue<Vector2>().x == 1 || movement.ReadValue<Vector2>().x == -1 || movement.ReadValue<Vector2>().y == 1 || movement.ReadValue<Vector2>().y == -1)
        {
            playerAnimator.SetFloat("lastMoveX", movement.ReadValue<Vector2>().x);
            playerAnimator.SetFloat("lastMoveY", movement.ReadValue<Vector2>().y);
        }
        if (movement.ReadValue<Vector2>().x == 0 & movement.ReadValue<Vector2>().y == 0)
        {
            //Do Nothing!

        }
        else
        {
            lastMove.x = movement.ReadValue<Vector2>().x; 
            lastMove.y = movement.ReadValue<Vector2>().y; 
        }
        if (isAttacking)
        {
            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0)
            {
                playerAnimator.SetBool("IsAttacking", false);
                //weaponAnimator.SetBool("IsAttacking", false);
                isAttacking = false;
                moveSpeed = 5f;
            }
        }
    }
    private void Attack(InputAction.CallbackContext obj)
    {
        attackCounter = attackTime;
      
        isAttacking = true;
        playerAnimator.SetBool("IsAttacking", true);

        //Stop the player moving while attacking
        moveSpeed = 0f;
        rb.velocity = new Vector2(0, 0);
        

        //ThrowBoomerang(); //Need to move this to an item

        soundNum = Random.Range(1, 5);
        audioMan.Play("SlashAttack" + soundNum);

    }
    public void ThrowBoomerang()
    {
        if(Boomerangs > 0)
        {
            Boomerangs -= 1;
            GameObject Rang = Instantiate(Boomerang, transform.position + new Vector3(0 + lastMove.x * 2, 0 + lastMove.y * 2, 0), Quaternion.identity);
            Rang.GetComponent<Rigidbody2D>().velocity = lastMove * BoomerangThrowSpeed;
            soundNum = Random.Range(1, 3);
            audioMan.Play("ThrowBoomerang" + soundNum);
        } 
    }

    private void ThrowBooomerang(InputAction.CallbackContext obj)
    {
        ThrowBoomerang();
    }

    private void PauseGame(InputAction.CallbackContext obj)
    {
        pMenu.getPing();
    }
}
