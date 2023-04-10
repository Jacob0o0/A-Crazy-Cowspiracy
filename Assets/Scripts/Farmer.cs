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
    private float counter;
    private float randomLook;
    private bool checkingReality;
    private float lookingAtCow;
    private float timeLooking;
    [Header("Aspect of the farmer")]
    private SpriteRenderer farmerSpriteR;
    public Sprite iddle;
    public Sprite looking;
    
    //Body of the cow
    [Header("Player")]
    public Transform cow;
    private Vector2 cowInitialPosition;

    //Variables of the game
    [Header("Game Variables")]
    public bool gameover;
    public bool knockout;
    private float timeReaction = 0.35f;
    private bool randomize;

    // Start is called before the first frame update
    void Start()
    {
        knockout = false;
        gameover = false;
        randomize = true;

        farmerSpriteR = GetComponent<SpriteRenderer>();

        counter = 0f;
        secondsAwake = 0f;
        checkingReality = false;
        lookingAtCow = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameover) // GAME OVER
        {

        }
        else // Still playing
        {
            if(!knockout)
            {
                // For the movement of the farmer
                secondsAwake += Time.deltaTime;
                moveSpeed = IncreaseSpeed(secondsAwake);
                //Debug.Log(moveSpeed);
                transform.position += Vector3.right * (moveSpeed) * Time.deltaTime;

                counter += Time.deltaTime;

                // RANDOM THINGS ONCE
                if (randomize)
                {
                    RandomThings();
                    randomize = false;
                }

                //Debug.Log(counter);

                // Checking if the cow moves
                if (counter > randomLook-0.1 && counter < randomLook+0.1) // Random time in between 3 seconds and 8 seconds for loock at the cow
                {
                    checkingReality = true; // To look at the cow
                }
                if (checkingReality) // Function to loock at the cow
                {
                    farmerSpriteR.sprite = looking; // to change the sprite of the farmer

                    lookingAtCow += Time.deltaTime; // time lookin at the cow
                    
                    if (lookingAtCow > timeReaction && lookingAtCow < timeLooking) // to check if the cow moves
                    {
                        if (lookingAtCow > timeReaction-0.05 && lookingAtCow < timeReaction+0.05)
                        {
                            cowInitialPosition = cow.position;
                            Debug.Log(cowInitialPosition);
                            Debug.Log("ALTO");
                        }

                        if (Vector2.Distance(cowInitialPosition, cow.position) > 0)
                        {
                            gameover = true; // GAME OVER
                            Debug.Log("GAME OVER");
                        }
                    }
                    else if (lookingAtCow >= timeLooking) // the farmer is crazy and he didn't see anything
                    {
                        counter = 0;
                        farmerSpriteR.sprite = iddle;
                        lookingAtCow = 0f;
                        
                        randomize = true;
                        checkingReality = false;
                    }
                }
                else
                {
                    farmerSpriteR.sprite = iddle;
                }
            }
        }
    }

    void OnCollisionTr2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("El objeto colisionó con el jugador.");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisión con vaca");
        }
    }

    float IncreaseSpeed(float seconds)
    {
        return initialSpeed + (Mathf.Pow(seconds, growthRate));
    }

    void RandomThings()
    {
        randomLook = Random.Range(3f, 8f); // To randomize the time that the farmer look at the cow in a range of 3 seconds to 8 seconds
        timeLooking = Random.Range(1.5f, 2.5f); // To randomize the time that the farmer spends looking at the cow
        randomize = false; // To not call the function again until the farmer looks at the cow

        Debug.Log("RANDOM LOOK AT:");
        Debug.Log(randomLook);
    }
}