using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // TO DO:
    // Make GUI to show menu of the game, add music, make animations of game over and add a sistem to save the high score.
    // Also, make animation of the cow eating corn and add score for that. Final score is corn eated x seconds.

    public static GameManager Instance; // To grab the instance of the game everywhere

    [Header("Characters")]
    public GameObject player;
    public GameObject farmer;

    [Header("Variables of the game")]
    public bool gameStart;
    public bool centinelaStart;
    public bool gameover;
    private float timePlaying;
    private bool timerOn;

    [Header("GUI")]
    public TMPro.TextMeshProUGUI timerText;
    public GameObject titleObj;
    public RectTransform title;
    public float moveSpeed = 150f;

    void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameStart = false;
        centinelaStart = false;
        gameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart && timerOn)
        {
            ChangeTimer();
        }

        if (gameStart && !centinelaStart)
        {
            title.anchoredPosition += new Vector2(0f, moveSpeed * Time.deltaTime);
            Vector3[] corners = new Vector3[4];
            title.GetWorldCorners(corners);
            if (corners[0].y > Screen.height) {
                Destroy(titleObj);
                centinelaStart = true;
                Debug.Log("Title moved");
            }
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
