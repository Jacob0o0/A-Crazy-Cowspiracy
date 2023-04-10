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

    //Variables of the game
    public bool gameover;
    public bool knockout;

    // Start is called before the first frame update
    void Start()
    {
        knockout = false;
        gameover = false;
        counter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameover) // GAME OVER
        {

        }
        else // Still playing
        {
            counter += Time.deltaTime;

            if(!knockout)
            {
                secondsAwake += Time.deltaTime;
                moveSpeed = IncreaseSpeed(secondsAwake);
                Debug.Log(moveSpeed);
                transform.position += Vector3.right * (moveSpeed) * Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("El objeto colisionó con el jugador.");
        }
    }

    float IncreaseSpeed(float seconds)
    {
        return initialSpeed + (Mathf.Pow(seconds, growthRate));
    }
}
