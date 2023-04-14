using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    // Variables for the cow
    [Header("Movement")]
    [SerializeField] private float initialSpeed = 3f;
    [SerializeField] private float growthRate = 0.4f;
    private float moveSpeed;
    private float counter;
    private Rigidbody2D rb;

    // Variables of the game
    public bool gameover;
    public float secondsPlaying;
    
    // Start is called before the first frame update
    void Start()
    {
        gameover = false;
        secondsPlaying = 0f;

        counter = 0f;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameover) // Still Playing
        {
            secondsPlaying += Time.deltaTime;
            counter += Time.deltaTime;
            
            moveSpeed = IncreaseSpeed(secondsPlaying);

            if (Input.GetKey(KeyCode.RightArrow)) // Verifica si se ha presionado la tecla derecha
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime; // Mueve el personaje a la derecha
            }
        }
        else // GAME OVER
        {
            
        }
    }

    private void FixedUpdate()
    {

    }

    float IncreaseSpeed(float seconds)
    {
        return initialSpeed + (Mathf.Pow(seconds, growthRate));
    }
}
