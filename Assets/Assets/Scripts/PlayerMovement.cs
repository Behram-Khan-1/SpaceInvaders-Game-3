using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of the player movement
    private float leftBound, rightBound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftBound = -3f;
        rightBound = 3f;
    }
    void FixedUpdate()
    {
        if (!GameManager.instance.canMove)
        {
            return; // Exit if movement is disabled
        }
        Movement();
    }
    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        // Debug.Log("Horizontal Input: " + horizontalInput);


        if (transform.position.x < leftBound )
        {
            if(horizontalInput < 0) horizontalInput = 0; // Prevent moving left beyond the left bound
        }
        if (transform.position.x > rightBound ) 
        {
            if(horizontalInput > 0) horizontalInput = 0; // Prevent moving right beyond the right bound
        }

        if (horizontalInput != 0)
        {
            transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);
        }


    }
}
