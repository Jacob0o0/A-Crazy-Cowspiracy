using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    //Variables for the farmer
    [Header("Movement")]
    [SerializeField] private float initialSpeed = 2f;
    [SerializeField] private float growthRate = 0.3f;
    private float moveSpeed;
    private float secondsAwake;
    private float counter; // auxiliar to look at the cow
    private float randomLook;
    private bool checkingReality;
    private float lookingAtCow;
    private float timeLooking;
    private int timesLookingAtCow; // the times that the farmer has look at the cow

    [Header("Aspect of the farmer")]
    private Animator animator;
    private SpriteRenderer farmerSpriteR;
    private GameObject headNormal;
    private GameObject headChecking;


    //Body of the cow
    [Header("Player")]
    public Cow player;
    public Transform cow;
    private Vector2 cowInitialPosition;

    //Variables of the game
    [Header("Game Variables")]
    public bool gameover;
    public bool gameStart;
    public bool knockout;
    private float timeReaction = 0.35f;
    private bool randomize;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        knockout = false;
        gameover = false;
        gameStart = false;
        randomize = true;
        mainCamera = Camera.main;

        farmerSpriteR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        headNormal = transform.GetChild(0).gameObject;
        headNormal.SetActive(false);
        headChecking = transform.GetChild(1).gameObject;
        headChecking.SetActive(false);

        counter = 0f;
        secondsAwake = 0f;
        checkingReality = false;
        lookingAtCow = 0f;
        timesLookingAtCow = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover && gameStart) // Still playing
        {
            // For the movement of the farmer
            animator.SetBool("Moving", true);
            headNormal.SetActive(true);
            secondsAwake += Time.deltaTime;
            moveSpeed = IncreaseSpeed(secondsAwake);
            //Debug.Log(moveSpeed);
            transform.position += Vector3.right * (moveSpeed) * Time.deltaTime;
            // ------------------------------

            counter += Time.deltaTime;

            // RANDOM THINGS ONCE
            if (randomize)
            {
                RandomThings();
                randomize = false;
            }
            // -------------------

            // Checking if the cow moves at a random time
            if (counter > randomLook - 0.5 && counter < randomLook + 0.5) // Random time in between 3 seconds and 8 seconds for loock at the cow
            {
                checkingReality = true; // To look at the cow
            }

            if (checkingReality) // Function to look at the cow
            {
                headNormal.SetActive(false);
                headChecking.SetActive(true);

                lookingAtCow += Time.deltaTime; // time lookin at the cow

                if (lookingAtCow > timeReaction && lookingAtCow < timeLooking) // to check if the cow moves
                {
                    if (lookingAtCow > timeReaction - 0.05 && lookingAtCow < timeReaction + 0.05)
                    {
                        cowInitialPosition = cow.position;
                    }

                    if (Vector2.Distance(cowInitialPosition, cow.position) > 0 && !gameover)
                    {
                        GameOver(); // GAME OVER
                    }
                }
                else if (lookingAtCow >= timeLooking) // the farmer is crazy and he didn't see anything
                {
                    counter = 0;
                    
                    lookingAtCow = 0f;
                    headChecking.SetActive(false);
                    headNormal.SetActive(true);
                    randomize = true;
                    checkingReality = false;
                    timesLookingAtCow += 1;
                }
            }
            // -----------------------------------------

            OutOfScreen(); // to check if the farmer is out of screen

        }
        else // GAME OVER
        {
            if (!gameStart) {
                
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !gameover)
        {
            GameOver();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!gameStart) // Function to make the game start
            {
                gameStart = true;
                GameManager.Instance.ActivateTimer();
                animator.SetBool("Moving", true);
                headNormal.SetActive(true);
            }
        }
    }

    float IncreaseSpeed(float seconds)
    {
        return initialSpeed + (Mathf.Pow(seconds, growthRate));
    }

    void RandomThings()
    {
        if (timesLookingAtCow < 1000)
        {
            randomLook = Random.Range(2f - (Mathf.Pow(timesLookingAtCow, growthRate) / 4f), 6f - (Mathf.Pow(timesLookingAtCow, 0.45f) * 0.26f)); // To randomize the time that the farmer look at the cow in a range of 3 seconds to 8 seconds
            timeLooking = Random.Range(0.50f, 2f - (Mathf.Pow(timesLookingAtCow, growthRate) / 6)); // To randomize the time that the farmer spends looking at the cow
            randomize = false; // To not call the function again until the farmer looks at the cow
        }
        else
        {
            // The player just win the game (idk how)
        }
    }

    void GameOver()
    {
        Debug.Log("GAME OVER");
        gameover = true;
        headChecking.SetActive(false);
        animator.SetBool("GameOver", true);
        player.gameover = true;
        GameManager.Instance.DeactivateTimer();
        GameManager.Instance.gameover = true;
    }

    void OutOfScreen()
    {
        Vector3 screenPosition = mainCamera.WorldToViewportPoint(transform.position);

        if(screenPosition.x > 1.1)
        {
            GameOver();
        }
    }
}