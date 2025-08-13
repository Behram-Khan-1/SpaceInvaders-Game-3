using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public BulletType bulletType;
    private Vector3 moveDirection;

    void Start()
    {
        Destroy(this.gameObject, 2f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    } 
    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.instance.BossBattleStart == true)
        {
            if (other.gameObject.tag == "Boss" && bulletType == BulletType.PlayerBullet)
            {
                GameManager.instance.DecreaseBossHealth();
                GameManager.instance.IncreaseScore(10);
                Destroy(this.gameObject);
            }
            if (other.gameObject.tag == "Shield" && bulletType == BulletType.PlayerBullet)
            {
                Destroy(this.gameObject);
            }
        }

            if (other.gameObject.tag == "Enemy" && bulletType == BulletType.PlayerBullet)
            {
                GameManager.instance.ResetExtremes();
                GameManager.instance.ResetActiveShooter(other.transform);
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                GameManager.instance.IncreaseScore(10);
                GameManager.instance.DecreaseEnemyCount();
            }
            else if (other.gameObject.tag == "Bullet" && bulletType == BulletType.EnemyBullet)
            {
                return; // Ignore enemy bullets hitting each other
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
