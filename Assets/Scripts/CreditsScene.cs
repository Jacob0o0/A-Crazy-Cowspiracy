using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScene : MonoBehaviour
{
    public static CreditsScene Instance;
    public bool soundStatus;

    void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start() {
        if (PlayerPrefs.GetInt("Music", 0) == 1) { // The sound is on
            soundStatus = true;
        } else if (PlayerPrefs.GetInt("Music", 0) == 0) { // The sound is off
            soundStatus = false;
        }
        MusicControl.Instance.PlayMainSong(soundStatus);
    }

    public void OpenInsta(){
        Application.OpenURL("https://www.instagram.com/alien_bones_px/");
    }

    public void ReturnMain() {
        SceneManager.LoadScene("Scenes/MainGame");
    }
}