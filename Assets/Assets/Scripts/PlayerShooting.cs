using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject Bullet;
    public float Speed = 20;
    public float fireRate;
    bool hasShot = false;
    private float initialFireRate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        initialFireRate = fireRate;
    }
    void Update()
    {
        if (!GameManager.instance.canShoot)
        {
            return; // Exit if shooting is disabled
        }
        if (hasShot == true)
        {
            fireRate = fireRate - Time.deltaTime;
            if (fireRate < 0)
            {
                hasShot = false;
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var spawnedBullet = Instantiate(Bullet, transform.position + new Vector3(0, 0.1f, 0), transform.rotation);
            spawnedBullet.GetComponent<Bullet>().speed = Speed;
            spawnedBullet.GetComponent<Bullet>().bulletType = BulletType.PlayerBullet;
            spawnedBullet.GetComponent<Bullet>().SetDirection(Vector3.up);
            hasShot = true;
            fireRate = initialFireRate;
        }
    }
}
