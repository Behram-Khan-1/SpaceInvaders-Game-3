using UnityEngine;

public class BossShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 2f;
    private float cooldown = 0;
    public Transform[] bulletSpawnPoints;

    void Update()
    {
        if (Boss.canShoot)
            Shoot();
    }

    public void Shoot()
    {
        cooldown = cooldown - Time.deltaTime;
        if (cooldown < 0)
        {
            SpawnBullet();
            cooldown = UnityEngine.Random.Range(fireRate - 0.5f, fireRate + 0.5f);
        }
    }

    public void SpawnBullet()
    {
        var spawnedBullet = Instantiate(bulletPrefab, this.transform.position + new Vector3(0, 0.1f, 0), transform.rotation);
        spawnedBullet.GetComponent<Bullet>().speed = bulletSpeed;
        spawnedBullet.GetComponent<Bullet>().bulletType = BulletType.EnemyBullet;
        spawnedBullet.GetComponent<Bullet>().SetDirection(Vector3.down);
    }
    public void SpawnBulletSpread()
    {
       for (int i = 0; i < bulletSpawnPoints.Length; i++)
        {
            var spawnedBullet = Instantiate(bulletPrefab, bulletSpawnPoints[i].position, bulletSpawnPoints[i].rotation);
            spawnedBullet.GetComponent<Bullet>().speed = bulletSpeed;
            spawnedBullet.GetComponent<Bullet>().bulletType = BulletType.EnemyBullet;
            Vector2 direction = -bulletSpawnPoints[i].up;
            spawnedBullet.GetComponent<Bullet>().SetDirection(direction);
        }
    }

}
