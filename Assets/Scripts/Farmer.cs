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

    [Header("Aspect of the farmer")]
    private Animator animator;
    private SpriteRenderer farmerSpriteR;
    private GameObject headNormal;
    private GameObject headChecking;
    private GameObject cornPlus;


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
    public bool collided;
    public float intervalToWait = 3.2f;
    private float timeReaction = 0.35f;
    private Camera mainCamera;

    [Header("Sound")]
    private AudioSource lookSound;
    private bool playingSound;

    // Start is called before the first frame update
    void Start()
    {
        knockout = false;
        gameover = false;
        gameStart = false;
        collided = false;
        mainCamera = Camera.main;

        farmerSpriteR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        headNormal = transform.GetChild(0).gameObject;
        headNormal.SetActive(false);
        headChecking = transform.GetChild(1).gameObject;
        headChecking.SetActive(false);
        cornPlus = transform.GetChild(2).gameObject;
        cornPlus.SetActive(false);

        moveSpeed = initialSpeed;
        counter = 0f;
        secondsAwake = 0f;
        checkingReality = false;
        lookingAtCow = 0f;

        lookSound = GetComponent<AudioSource>();
        playingSound = false;
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
            transform.position += Vector3.right * (moveSpeed) * Time.deltaTime;
            // ------------------------------

            counter += Time.deltaTime;

            // The farmer looks at the cow with a probabilty based on his position between the cow and the end of the screen
            if (counter > intervalToWait) {
                Debug.Log($"Segundos: {counter}");
                Debug.Log($"Espera: {intervalToWait}");
                int randomNumber = Random.Range(0, 101); // random number between 0 and 100
                RandomThings(randomNumber);
                counter = 0;
            }

            if (checkingReality) { // Function to look at the cow
                LookAtTheCow();
            } else if (collided) {
                int randomNumber = Random.Range(0, 101); // random number between 0 and 100
                RandomThings(randomNumber);
                LookAtTheCow();
            }
            // -------------------------------

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
                collided = true;
                GameManager.Instance.ActivateTimer();
                animator.SetBool("Moving", true);
                headNormal.SetActive(true);
            }

            collided = true;
            GameManager.Instance.corns += 1;
            cornPlus.SetActive(true);
            StartCoroutine(DeactivateCorn());
        }
    }

    IEnumerator DeactivateCorn() {
        yield return new WaitForSeconds(1);
        cornPlus.SetActive(false);
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine(DeactivateCorn());
            cornPlus.SetActive(false);
            SoundEfects.Instance.CornPlusSound();
        }
    }

    float IncreaseSpeed()
    {
        // return initialSpeed + (Mathf.Pow(seconds, growthRate));
        return initialSpeed + 0.2f;
    }

    void RandomThings(int randomInt)
    {
        if (collided) {
            checkingReality = true;
            timeLooking = Random.Range(0.50f, 2.5f); // To randomize the time that the farmer spends looking at the cow
        } else {
            // Get the coordinates of the right edge of the screen in camera space with a gap in x axis
            Vector3 screenRight = new Vector3(0.9f, 0.5f, 0);
            Vector3 screenLeft = new Vector3(0f, 0.5f, 0);
            Vector3 worldPointR = mainCamera.ViewportToWorldPoint(screenRight);
            Vector3 worldPointL = mainCamera.ViewportToWorldPoint(screenLeft);

            float totalDistance = worldPointR.x - worldPointL.x; // Space between the cow and the end of the camera view
            float farmerDistance = worldPointR.x - transform.position.x; // Distance between the farmer and the end of the camera view
            float percentage = (farmerDistance / totalDistance) * 100f; // Percentage of the distance (PROBABILITY that the farmer looks at the cow)
            float probability = percentage + (GameManager.Instance.corns/2);
            Debug.Log($"Probability: {probability}");

            if (randomInt <= probability) {
                checkingReality = true;
                if (probability >= (50/3)) {
                    timeLooking = Random.Range(0.50f, ((probability * 3)/100) ); // To randomize the time that the farmer spends looking at the cow
                } else {
                    timeLooking = 0f;
                }
            }
        }
    }

    void LookAtTheCow() {
        headNormal.SetActive(false);
        if (playingSound == false)
        {
            lookSound.Play();
            playingSound = true;
        }
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
            lookingAtCow = 0f;
            lookSound.Stop();
            playingSound = false;
            checkingReality = false;
            collided = false;
            intervalToWait = intervalToWait - 0.02f;

            headChecking.SetActive(false);
            headNormal.SetActive(true);
            
            moveSpeed = IncreaseSpeed();
            player.IncreaseSpeed();
        }
    }

    void OutOfScreen()
    {
        Vector3 screenPosition = mainCamera.WorldToViewportPoint(transform.position);

        if(screenPosition.x > 1.15)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("GAME OVER");
        gameover = true;

        headChecking.SetActive(false);
        headNormal.SetActive(false);
        animator.SetBool("GameOver", true);
        GameManager.Instance.DeactivateTimer();
        GameManager.Instance.gameover = true;
    }

}