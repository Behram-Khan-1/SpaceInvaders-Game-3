using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float snapDistanceX = 0.3f;
    public float snapDistanceY = 0.5f;
    public int movementDirection = 1;
    public float leftBound, rightBound;
    public float Timer = 0.5f;
    private float initialTimer;

    [SerializeField] Enemy leftMostEnemy = null;
    [SerializeField] Enemy rightMostEnemy = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialTimer = Timer;
        GetExtremeChilds();
    }

    // Update is called once per frame
    void Update()
    {
        Timer = Timer - Time.deltaTime;
        // Debug.Log("Timer: " + Timer);
        if (Timer < 0)
        {
            Timer = initialTimer;
            Movement();
        }

    }

    private void Movement()
    {
        transform.position = new Vector2(transform.position.x + snapDistanceX * movementDirection, transform.position.y);

        if (rightMostEnemy == null || leftMostEnemy == null)
        {
            GetExtremeChilds();
        }

        if (Vector2.Distance(rightMostEnemy.transform.position, new Vector2(rightBound, rightMostEnemy.transform.position.y)) < 0.1f
                || Vector2.Distance(leftMostEnemy.transform.position, new Vector2(leftBound, rightMostEnemy.transform.position.y)) < 0.1f)
        {
            movementDirection *= -1;
            transform.position = new Vector2(transform.position.x, transform.position.y + snapDistanceY);
            GameManager.instance.EnemyMoveToBottom();
        }
            
        
    }

    public void GetExtremeChilds()
    {
        List<Transform> totalEnemies = TotalEnemies();
        
        float leftMost = float.MaxValue;
        float rightMost = float.MinValue;

        foreach (Transform enemy in totalEnemies)
        {
            // Debug.Log("Enemy Position: " + enemy.name);
            if (enemy.position.x < leftMost)
            {
                leftMost = enemy.position.x;
                leftMostEnemy = enemy.GetComponent<Enemy>();
            }

            if (enemy.position.x > rightMost)
            {
                rightMost = enemy.position.x;
                rightMostEnemy = enemy.GetComponent<Enemy>();
            }

        }
    }
    

    public List<Transform> TotalEnemies()
    {
        int totalRows = transform.childCount;
        List<Transform> totalEnemies = new List<Transform>();

        for (int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                totalEnemies.Add(transform.GetChild(i).GetChild(j));
                // Debug.Log("Enemy: " + transform.GetChild(i).GetChild(j).name);
            }
        }
        return totalEnemies;
    }
    
    public List<Transform> TotalRows()
    {
        int totalRows = transform.childCount;
        List<Transform> totalRowsList = new List<Transform>();

        for (int i = 0; i < totalRows; i++)
        {
            totalRowsList.Add(transform.GetChild(i));
        }
        return totalRowsList;
    }
}


