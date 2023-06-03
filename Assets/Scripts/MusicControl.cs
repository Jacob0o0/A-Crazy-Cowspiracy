using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicControl : MonoBehaviour
{
    public static MusicControl Instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip mainSong;
    [SerializeField] private AudioClip gameoverSong;
    [SerializeField] private AudioClip runSong;
    private bool music;

    private void Awake(){
        if (Instance == null){
            Instance = this;

            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Start(){
        audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("Music", 0) == 1) { // The sound is on
            music = true;
            PlayMainSong(music);
        } else if (PlayerPrefs.GetInt("Music", 0) == 0) { // The sound is off
            music = false;
        }
    }

    void Update(){

    }

    public void PlayMainSong(bool play){
        if (play && music == true)
        {
            audioSource.clip = mainSong;
            audioSource.loop = true;
            audioSource.Play();
        } else if (!play)
        {
            audioSource.Stop();
        }
    }

    public void PlayRunSong(bool play){
        if(play && music == true)
        {
            audioSource.clip = runSong;
            audioSource.loop = true;
            audioSource.Play();
        } else if (!play)
        {
            audioSource.Stop();
        }
    }

    public void GameOverSong(bool play){
        if(play && music == true)
        {
            audioSource.PlayOneShot(gameoverSong);
        } else if (!play) 
        {
            audioSource.Stop();
        }
    }

    public void StopMusic(){
        audioSource.Stop();
    }

    public void Music(bool status){
        if (status)
        {
            music = status;
            PlayMainSong(true);
        } else if (status == false) 
        {
            audioSource.Stop();
            music = status;
        }
    }
}