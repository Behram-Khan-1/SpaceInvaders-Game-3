using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float snapDistanceX = 0.3f;
    public float snapDistanceY = 0.5f;
    public int movementDirection = 1;
    public float leftBound, rightBound;
    public float Timer = 0.5f;
    private float initialTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialTimer = Timer;
    }

    // Update is called once per frame
    void Update()
    {
        Timer = Timer - Time.deltaTime;
        Debug.Log("Timer: " + Timer);
        if (Timer < 0)
        {
            Timer = initialTimer;
            Movement();
        }

    }

    private void Movement()
    {
        transform.position = new Vector2(transform.position.x + snapDistanceX * movementDirection, transform.position.y);

        if(Vector2.Distance(transform.position, new Vector2(rightBound, transform.position.y)) < 0.1f
            || Vector2.Distance(transform.position, new Vector2(leftBound, transform.position.y)) < 0.1f)
        {
            movementDirection *= -1;
            transform.position = new Vector2(transform.position.x, transform.position.y + snapDistanceY);
        }
        
    }
}
