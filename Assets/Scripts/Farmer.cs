using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour
{
    //Variables for the farmer
    public float moveSpeed = 5f;
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
                transform.position += Vector3.right * (moveSpeed+(counter/1000)) * Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.tag == "Player") {
        Debug.Log("El objeto colisionó con el jugador.");
    }
}
}
