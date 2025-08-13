using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float movementSpeed = 2f; // Speed of the boss movement
    public float leftBound, rightBound;
    private float movementDirection = 1f; // 1 for right, -1 for left

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Boss.goingToCenter)
        {
            Vector2 targetPos = new Vector2(0, -2);
            Boss.canShoot = false;
            Boss.canMove = false;
            GameManager.instance.canMove = false;
            GameManager.instance.canShoot = false;

            transform.parent.position = Vector2.Lerp(transform.parent.position, targetPos, 1.5f);
            if (Vector2.Distance(transform.parent.position, targetPos) < 0.1f)
                Boss.goingToCenter = false;

            GameManager.instance.canMove = true;
            GameManager.instance.canShoot = true;
        }
        
        if (Boss.canMove == false)
            return;


        Movement();
    }

    private void Movement()
    {
        // Debug.Log("Boss is moving");
        transform.parent.Translate(Vector2.right * movementDirection * movementSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.parent.position, new Vector2(rightBound, transform.position.y)) < 0.5f ||
           Vector2.Distance(transform.parent.position, new Vector2(leftBound, transform.position.y)) < 0.5f)
        {
            movementDirection *= -1; // Change direction when hitting bounds
        }
    }



}

