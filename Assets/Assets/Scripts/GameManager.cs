using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public EnemyMovement enemyMovement;
    public EnemyShooting enemyShooting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ResetExtremes()
    {
        enemyMovement.GetExtremeChilds();
    }
    public void ResetActiveShooter(Transform transform)
    {
        enemyShooting.ResetActiveShooters(transform);
    }
}
