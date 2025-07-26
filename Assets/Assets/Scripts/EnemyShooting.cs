using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 5f;
    private float cooldown = 0;
    EnemyMovement enemyMovement;
    public List<Transform> activeShooters = new List<Transform>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        ActiveShooters();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown = cooldown - Time.deltaTime;
        if (cooldown < 0)
        {
            int randomIndex = Random.Range(0, activeShooters.Count);
            var shooter = activeShooters[randomIndex];

            var spawnedBullet = Instantiate(bulletPrefab, shooter.position + new Vector3(0, 0.1f, 0), transform.rotation);
            spawnedBullet.GetComponent<Bullet>().speed = bulletSpeed;
            spawnedBullet.GetComponent<Bullet>().bulletType = BulletType.EnemyBullet;
            cooldown = fireRate;
        }
    }

    void ActiveShooters()
    {
        var rows = enemyMovement.TotalRows();
        Transform lastRow = rows.Last();
        foreach (Transform enemy in lastRow)
        {
            activeShooters.Add(enemy);
        }
    }

    public void ResetActiveShooters(Transform deadShooter)
    {
        List<Transform> rows = enemyMovement.TotalRows();
        if (rows.IndexOf(deadShooter.parent) - 1 < 0)
        {
            activeShooters.Remove(deadShooter);
            return;
        }
        int secondLastRowIndex = rows.IndexOf(deadShooter.parent) - 1;
        Transform secondLastRow = rows[secondLastRowIndex];

        // if (secondLastRow == null)
        // {
        //     activeShooters.Remove(deadShooter);
        //     return;
        // }

        foreach (Transform enemy in secondLastRow)
            {
                if (enemy.position.x == deadShooter.position.x)
                {
                    activeShooters.Remove(deadShooter);
                    activeShooters.Add(enemy);
                }
            }
        foreach (Transform row in rows)
        {
            if(row.childCount == 0)
            {
                Destroy(row.gameObject);
            }
        }

    }


}