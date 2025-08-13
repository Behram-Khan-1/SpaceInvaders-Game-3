using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public EnemyMovement enemyMovement;
    public EnemyShooting enemyShooting;
    private Boss boss;
    int health = 5;
    int score = 0;
    float bottomBound = -4;
    int totalEnemies = 0;
    bool HasBossBattle = true;
    public bool BossBattleStart = false;
    public GameObject TopUI;
    public List<Sprite> healthSprites;
    public GameObject healthSprite;
    public GameObject bossHealthSprite;
    public GameObject BossPrefab;
    public bool canMove = true;
    public bool canShoot = true;

    public TextMeshProUGUI scoreText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        totalEnemies = enemyMovement.TotalEnemies().Count;
    }


    public void DecreaseEnemyCount()
    {
        totalEnemies--;
        if (totalEnemies <= 0)
        {
            if (HasBossBattle)
            {
                // Trigger boss battle logic here
                Debug.Log("Boss Battle Triggered!");
                StartBossBattle();
            }
            else
            {
                GameWin();
            }
        }
    }
    public void StartBossBattle()
    {
        BossBattleStart = true;
        GameObject bosss = Instantiate(BossPrefab, new Vector3(0, 2, 0), Quaternion.identity);
        boss = bosss.GetComponentInChildren<Boss>();
        Destroy(enemyMovement.gameObject);
        CanPlayerMove(false);
        TopUI.SetActive(false);
        bossHealthSprite.SetActive(true);

        Debug.Log(boss.name);
    }

    public void CanPlayerMove(bool isStop)
    {
        canMove = isStop;
        canShoot = isStop;
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
    private void GameOver()
    {
        Debug.Log("Game Over");
        // Implement game over logic here, like restarting the game or showing a game over screen
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
        healthSprite.GetComponent<SpriteRenderer>().sprite = healthSprites[health];
        if (health <= 0)
        {
            GameOver();
        }
    }

    public void DecreaseBossHealth()
    {
        boss.health--;
        int healthIndex = Mathf.Clamp((int)Mathf.Ceil((boss.health - 1) / 10.0f), 0, healthSprites.Count - 1);
        bossHealthSprite.GetComponent<SpriteRenderer>().sprite = healthSprites[healthIndex];
        if (boss.health <= 0)
        {
            Destroy(boss.gameObject);
            GameWin();
        }
        boss.ChangePhase();
    }

    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
        Debug.Log("Score: " + score);
        // Update UI or other game elements with the new score
    }

}
