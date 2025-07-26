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

    Enemy leftMostEnemy = null;
    Enemy rightMostEnemy = null;
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
            }
    }

    public void GetExtremeChilds()
    {
        List<Transform> rows = TotalRows();

        foreach (Transform row in rows)
        {
            float leftMost = float.MaxValue;
            float rightMost = float.MinValue;



            foreach (Transform enemyTransform in row)
            {
                float x = enemyTransform.position.x;
                Enemy enemy = enemyTransform.GetComponent<Enemy>();

                if (x < leftMost)
                {
                    leftMost = x;
                    leftMostEnemy = enemy;
                }

                if (x > rightMost)
                {
                    rightMost = x;
                    rightMostEnemy = enemy;
                }
            }

            // Then, reset all to false
            foreach (Transform enemyTransform in row)
            {
                enemyTransform.GetComponent<Enemy>().SetIsExtreme(false);
            }

            // Finally, mark the two extremes
            if (leftMostEnemy != null) leftMostEnemy.SetIsExtreme(true);
            if (rightMostEnemy != null) rightMostEnemy.SetIsExtreme(true);
        }
    }

    public List<Transform> TotalRows()
    {
        int totalRows = transform.childCount;
        List<Transform> rows = new List<Transform>();

        for (int i = 0; i < totalRows; i++)
        {
            rows.Add(transform.GetChild(i));
        }

        return rows;
    }
}


