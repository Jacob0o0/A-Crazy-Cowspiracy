using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    // Variables for the cow
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow)) // Verifica si se ha presionado la tecla derecha
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime; // Mueve el personaje a la derecha
        }
    }

    private void FixedUpdate()
    {

    }
}
