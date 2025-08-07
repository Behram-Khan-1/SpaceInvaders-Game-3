using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = Random.Range(0.8f, 1.2f);
    }
  



   
}
