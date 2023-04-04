using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollGround : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier; // percentage of the camera speed
    private Transform cameraTransform;
    private Vector3 previousCameraPosition;
    private float spriteWidth, startPosition; // variables for infinite scroll

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x; // size of the sprite
        startPosition = transform.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float moveAmount = cameraTransform.position.x; // where is actually the layer of the bg

        if(moveAmount > startPosition + spriteWidth)
        {
            transform.Translate(new Vector3(spriteWidth, 0, 0));
            startPosition += spriteWidth;
        }
    }
}
