using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement
    private Rigidbody rb; // Reference to the Rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void Update()
    {
        // Get input from WASD or Arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        float moveVertical = Input.GetAxis("Vertical"); // W/S or Up/Down arrows

        // Create a movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * moveSpeed;

        // Move the player
        rb.MovePosition(transform.position + movement * Time.deltaTime);
    }
}