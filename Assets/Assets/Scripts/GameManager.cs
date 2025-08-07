using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public EnemyMovement enemyMovement;
    public EnemyShooting enemyShooting;
    int health = 3;
    int score = 0;
    float bottomBound = -4;
    int totalEnemies = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        var enemies = enemyMovement.TotalEnemies();
        foreach (Transform enemy in enemies)
        {
            totalEnemies = enemyMovement.TotalEnemies().Count;
        }
    }

    public void ResetExtremes()
    {
        enemyMovement.GetExtremeChilds();
    }
    public void ResetActiveShooter(Transform transform)
    {
        enemyShooting.ResetActiveShooters(transform);
    }
    public void DecreaseHealth()
    {
        health--;
        if (health <= 0)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        Debug.Log("Game Over");
        // Implement game over logic here, like restarting the game or showing a game over screen
    }
    public void IncreaseScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
        // Update UI or other game elements with the new score
    }

    public void EnemyMoveToBottom()
    {
        List<Transform> bottomEnemies = enemyShooting.GetActiveShooters();
        foreach (Transform enemy in bottomEnemies)
        {
            if (enemy.position.y <= bottomBound)
            {
                GameOver();
            }
        }
    }
    public void GameWin()
    {
        Debug.Log("You Win!");

    }

}
