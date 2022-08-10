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
    public SpriteRenderer playerSpriteRenderer;

    private float attackTime = 0.9f;
    private float attackCounter = 1f;
    [SerializeField] private bool isAttacking;
    public GameObject Boomerang; //Boomerang prefab
    public GameObject RegularBomb; //Regular Bomb prefab 
    public float BoomerangThrowSpeed; //How fast does the boomerang move once spawned
    

    [SerializeField] private InputActions movementAction;
    [SerializeField] private InputAction movement;
    public Vector2 lastMove; //The direction we our facing based on our last move.
    public float moveSpeed = 5;

    [SerializeField] private PauseMenu pMenu;
    [SerializeField] private UIManager UIMan;
    private AudioManager audioMan;
    private int soundNum;

    [SerializeField] private GameObject SlashUp; //Do we use this? I dont think so need to double check

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        audioMan = FindObjectOfType<AudioManager>();
        pMenu = FindObjectOfType<PauseMenu>(true); //Dont forget to enable the Pause menu container gameobject, only the panel should ever be disabled.
        UIMan = FindObjectOfType<UIManager>(); //Maybe I should make the UI man a singleton
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

        movementAction.Player.PlaceBomb.performed += PlaceBomb;
        movementAction.Player.PlaceBomb.Enable();

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
        if(PlayerStats.Instance.Boomerangs > 0)
        {
            PlayerStats.Instance.Boomerangs -= 1;
            GameObject Rang = Instantiate(Boomerang, transform.position + new Vector3(0 + lastMove.x * 2, 0 + lastMove.y * 2, 0), Quaternion.identity);
            Rang.GetComponent<Rigidbody2D>().velocity = lastMove * BoomerangThrowSpeed;
            soundNum = Random.Range(1, 3);
            audioMan.Play("ThrowBoomerang" + soundNum);
            UIMan.BoomerangCounterUpdate();
        } 
    }

    private void ThrowBooomerang(InputAction.CallbackContext obj)
    {
        ThrowBoomerang();
    }

    private void PlaceBomb(InputAction.CallbackContext obj)
    {
        if (PlayerStats.Instance.regularBombs > 0)
        {
            PlayerStats.Instance.regularBombs -= 1;
            Instantiate(RegularBomb, transform.position + new Vector3(0 + lastMove.x * 1, 0 + lastMove.y * 1, 0), Quaternion.identity);
            UIMan.BombCounterUpdate();
            
        }
    }

    private void PauseGame(InputAction.CallbackContext obj)
    {
        pMenu.getPing();
    }
}
