using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public BulletType bulletType;

    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (bulletType  == BulletType.PlayerBullet)
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        else
            transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && bulletType == BulletType.PlayerBullet)
        {
            GameManager.instance.ResetExtremes();
            GameManager.instance.ResetActiveShooter(other.transform);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            GameManager.instance.IncreaseScore(10);
        }
        else if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Player" && bulletType == BulletType.EnemyBullet)
        {
            GameManager.instance.DecreaseHealth();
            Destroy(this.gameObject);
        }
        // else
        //lose HP

    }

}
