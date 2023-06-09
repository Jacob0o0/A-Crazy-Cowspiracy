using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cow : MonoBehaviour
{
    // Variables for the cow
    [Header("Movement")]
    [SerializeField] private float initialSpeed = 3f;
    [SerializeField] private float growthRate = 0.85f;
    private float moveSpeed;
    private float counter;
    private Rigidbody2D rb;
    [Header("Animation")]
    private Animator animator;

    [Header("Variables of the game")]
    public float secondsPlaying;
    private bool centinelaStart;

    [Header("GUI")]
    public Button menuButton;
    public bool walkingCentinela;
    public bool centinelaGameOver;

    // Start is called before the first frame update
    void Start()
    {
        centinelaStart = false;
        secondsPlaying = 0f;
        moveSpeed = initialSpeed;
        walkingCentinela = false;
        centinelaGameOver = false;

        counter = 0f;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.gameover) // Still Playing
        {
            secondsPlaying += Time.deltaTime;
            counter += Time.deltaTime;

            if (GameManager.Instance.touchDevice) // Playing on mobile device
            {
                if (EventSystem.current.currentSelectedGameObject == null) // Ignore GUI buttons 
                {
                    if (Input.touchCount > 0) // Verifica si se tocó la pantalla
                    {
                        if (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved)
                        {
                            if (centinelaStart == false)
                            {
                                GameManager.Instance.gameStart = true;
                                centinelaStart = true;
                            }
                            animator.SetBool("Moving", true);
                            transform.position += Vector3.right * moveSpeed * Time.deltaTime; // Mueve el personaje a la derecha
                            if (walkingCentinela == false) 
                            {
                                MusicControl.Instance.PlayMainSong(false);
                                MusicControl.Instance.PlayRunSong(true);
                                walkingCentinela = true;
                            }
                        }
                        else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                        {
                            if(walkingCentinela == true){
                                animator.SetBool("Moving", false);
                                MusicControl.Instance.PlayRunSong(false);
                                MusicControl.Instance.PlayMainSong(true);
                                walkingCentinela = false;
                            }
                        }
                    }
                    else
                    {
                        if(walkingCentinela == true){
                            animator.SetBool("Moving", false);
                            MusicControl.Instance.PlayRunSong(false);
                            MusicControl.Instance.PlayMainSong(true);
                            walkingCentinela = false;
                        }
                    }
                }
            }
            else // Playing on computer
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    if (centinelaStart == false)
                    {
                        GameManager.Instance.gameStart = true;
                        centinelaStart = true;
                    }
                    transform.position += Vector3.right * moveSpeed * Time.deltaTime; // Mueve el personaje a la derecha
                    if (walkingCentinela == false) 
                    {
                        animator.SetBool("Moving", true);
                        MusicControl.Instance.PlayMainSong(false);
                        MusicControl.Instance.PlayRunSong(true);
                        walkingCentinela = true;
                    }
                }
                else
                {
                    if(walkingCentinela == true)
                    {
                        animator.SetBool("Moving", false);
                        MusicControl.Instance.PlayRunSong(false);
                        MusicControl.Instance.PlayMainSong(true);
                        walkingCentinela = false;
                    }
                }
            }
        }
        else // GAME OVER
        {
            if(centinelaGameOver == false)
            {
                animator.SetBool("GameOver", true);
                MusicControl.Instance.StopMusic();
                MusicControl.Instance.GameOverSong(true);
                centinelaGameOver = true;
            }
        }
    }

    private void FixedUpdate()
    {

    }

    public void IncreaseSpeed()
    {
        moveSpeed = initialSpeed + 0.26f;
    }
}
