using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundEfects : MonoBehaviour
{
    public static SoundEfects Instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip cornPlus;
    [SerializeField] private AudioClip touch;

    private void Awake(){
        if (Instance == null){
            Instance = this;

            Debug.Log("INSTANCIA");
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void TouchGUISound(){
        Debug.Log("WHYYY?");
        audioSource.PlayOneShot(touch);
    }

    public void CornPlusSound(){
        audioSource.PlayOneShot(cornPlus);
    }

    public static AudioSource GetSoundObject()
    {
        return Instance.audioSource;
    }

}