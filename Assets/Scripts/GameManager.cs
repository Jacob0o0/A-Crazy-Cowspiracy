using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // TO DO:
    // Make GUI to show menu of the game, add music, make animations of game over and add a sistem to save the high score.
    // Also, make animation of the cow eating corn and add score for that. Final score is corn eated x seconds.

    public static GameManager Instance; // To grab the instance of the game everywhere

    // Characters
    public GameObject player;
    public GameObject farmer;

    // Variables of the Game
    public bool gameStart;
    public bool gameover;
    private float timePlaying;
    private bool timerOn;
    public TMPro.TextMeshProUGUI timerText;

    void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart && timerOn)
        {
            ChangeTimer();
        }
    }

    private void ChangeTimer()
    {
        timePlaying += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timePlaying / 60f);
        int seconds = Mathf.FloorToInt(timePlaying % 60f);

        // Show time on screen
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ActivateTimer()
    {

        timerOn = true;
    }

    public void DeactivateTimer()
    {
        timerOn = false;
    }
}
