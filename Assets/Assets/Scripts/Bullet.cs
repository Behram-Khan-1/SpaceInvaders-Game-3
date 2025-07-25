using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

}
