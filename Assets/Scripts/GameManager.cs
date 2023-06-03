using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool centinelaOver;
    private float timePlaying;
    private bool timerOn;
    public int corns;
    private int score;
    public bool touchDevice;

    [Header("GUI")]
    public TMPro.TextMeshProUGUI cornText;
    public TMPro.TextMeshProUGUI highscoreText;
    public TMPro.TextMeshProUGUI scoreText;
    public GameObject titleObj;
    public RectTransform title;
    public GameObject gameOverMenuObj;
    public RectTransform gameOverMenu;
    public GameObject optionsObj;
    public RectTransform options;
    public GameObject menuButton;
    public GameObject creditsButton;
    public GameObject cornCount;
    public GameObject highscoreObj;
    public GameObject newScorePrompt;
    public TMPro.TextMeshProUGUI soundText;
    public bool soundStatus;
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
        if (Instance == null)
        {
            Instance = this;
        }

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            touchDevice = true;
        }
        else if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            touchDevice = false;
        }

        // Verificar si es la primera vez que se ejecuta el juego
        if (PlayerPrefs.GetInt("IsFirstTime", 0) == 0)
        {
            // Realizar la configuración inicial
            SetupPlayerPrefs();

            // Establecer la variable de control para indicar que ya se ha configurado
            PlayerPrefs.SetInt("IsFirstTime", 1);
            PlayerPrefs.Save();
        }

        gameStart = false;
        centinelaStart = false;
        gameover = false;
        centinelaOver = false;
        corns = 0;
        soundStatus = true;

        gameOverMenuObj.SetActive(false);
        menuButton.SetActive(true);
        creditsButton.SetActive(true);
        optionsObj.SetActive(false);
        cornCount.SetActive(false);
        highscoreObj.SetActive(false);
        newScorePrompt.SetActive(false);

        if (PlayerPrefs.GetInt("Music", 0) == 1) { // The sound is on
            soundText.text = "Music\n on";
            soundStatus = true;
            MusicControl.Instance.PlayMainSong(true);
        } else if (PlayerPrefs.GetInt("Music", 0) == 0) { // The sound is off
            soundText.text = "Music\n off";
            soundStatus = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover) // Still playing
        {
            if (gameStart && timerOn)
            {
                ChangeCornText();
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
                menuButton.SetActive(false);
                creditsButton.SetActive(false);
                cornCount.SetActive(true);
            }
        }
        else if (gameover && !centinelaOver)// GAME OVER 
        {
            gameOverMenuObj.SetActive(true);
            menuButton.SetActive(true);
            creditsButton.SetActive(true);

            score = corns;
            highscoreObj.SetActive(true);
            UpdateHighScore();
            ShowScore();

            centinelaOver = true;
        }
    }

    private void SetupPlayerPrefs()
    {
        PlayerPrefs.SetInt("Music", 1);
    }

    private void ChangeCornText()
    {
        cornText.text = corns.ToString();
    }

    public void ActivateTimer()
    {

        timerOn = true;
    }

    public void DeactivateTimer()
    {
        timerOn = false;
    }

    private void UpdateHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0)) 
        {
            PlayerPrefs.SetInt("HighScore", score);
            newScorePrompt.SetActive(true);
        }

        highscoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    private void ShowScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void Menu()
    {
        if (PlayerPrefs.GetInt("Music", 0) == 1) { // The sound is on
            soundText.text = "Music\n on";
        } else if (PlayerPrefs.GetInt("Music", 0) == 0) { // The sound is off
            soundText.text = "Music\n off";
        }
        optionsObj.SetActive(true);
    }

    public void CloseMenu()
    {
        optionsObj.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Scenes/MainGame");
    }

    public void CreditsMenu()
    {
        SceneManager.LoadScene("Scenes/Credits");
    }

    public void MainSong()
    {

    }

    public void SoundControl()
    {
        Debug.Log("ENTRO A SOUND CONTROL");
        if (soundStatus) { // The sound is on
            soundStatus = false; // Sound off
            PlayerPrefs.SetInt("Music", 0);
            MusicControl.Instance.Music(soundStatus);
            soundText.text = "Music\n off";
        } else { // The sound is off
            soundStatus = true; // Sound on
            PlayerPrefs.SetInt("Music", 1);
            MusicControl.Instance.Music(soundStatus);
            soundText.text = "Music\n on";
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
