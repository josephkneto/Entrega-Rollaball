using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Speed at which the player moves.
    public float speed = 0; 

    public TextMeshProUGUI countText;

    private Rigidbody rb;

    private int count;

    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue) {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();
        // Store the X and Y components of the movement.
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

    void SetCountText() {
        countText.text =  "Count: " + count.ToString();
        if (count >= 9) {
            SceneManager.LoadScene(3);
        }
    }

    // FixedUpdate is called once per fixed frame-rate frame
    private void FixedUpdate() {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        } else if (other.gameObject.CompareTag("Enemy")) {
            SceneManager.LoadScene(2);
        } else if (other.gameObject.CompareTag("Fall")) {
            SceneManager.LoadScene(2);
        }
    }

}
