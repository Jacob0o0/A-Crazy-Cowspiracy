using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier; // percentage of the camera speed
    private Transform cameraTransform;
    private Vector3 previousCameraPosition;
    private float spriteWidth, startPosition; // variables for infinite scroll

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x; // size of the sprite
        startPosition = transform.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - previousCameraPosition.x) * parallaxMultiplier; // for parallax effect
        float moveAmount = cameraTransform.position.x * (1 - parallaxMultiplier); // where is actually the layer of the bg

        transform.Translate(new Vector3(deltaX, 0, 0)); // parallax effect working
    
        previousCameraPosition = cameraTransform.position; // making new camera position to be the old one

        if(moveAmount > startPosition + spriteWidth)
        {
            transform.Translate(new Vector3(spriteWidth, 0, 0));
            startPosition += spriteWidth;
        }
    }
}
