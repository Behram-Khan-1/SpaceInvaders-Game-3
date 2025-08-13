using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static BossPhase currentPhase = BossPhase.Phase1;
    private bool introPlayed = false;
    public static bool canShoot = false;
    public static bool canMove = false;
    public static bool goingToCenter = false;
    private Vector2 targetPos = new Vector2(0, 4);
    Animator animator;
    public int health = 50;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
        // Play the intro animation when the boss is instantiated
    }

    public void IntroFinished()
    {
        Debug.Log("Intro Finished");
        introPlayed = true;
        canShoot = true; // Allow shooting after the intro animation is finished
        canMove = true; // Allow movement after the intro animation is finished
        GameManager.instance.canMove = true; // Enable player movement after the intro
        GameManager.instance.canShoot = true; // Enable player shooting after the intro
        GameManager.instance.TopUI.SetActive(true);
    }

    public void ChangePhase()
    {
        if (health <= 40 && currentPhase == BossPhase.Phase1)
        {
            SwitchPhase(BossPhase.Phase2);
        }
        else if (health <= 30 && currentPhase == BossPhase.Phase2)
        {
            SwitchPhase(BossPhase.Phase3);
        }
        else if (health <= 20 && currentPhase == BossPhase.Phase3)
        {
            SwitchPhase(BossPhase.Phase4);
        }
        else if (health <= 10 && currentPhase == BossPhase.Phase4)
        {
            SwitchPhase(BossPhase.Phase5);
        }
    }


    public void SwitchPhase(BossPhase newPhase)
    {
        currentPhase = newPhase;

        var movement = GetComponentInChildren<BossMovement>();
        var shooter = GetComponentInChildren<BossShooter>();

        switch (currentPhase)
        {
            case BossPhase.Phase1:
                // Initial phase settings
                break;

            case BossPhase.Phase2:
                movement.movementSpeed = 1.5f;
                shooter.fireRate = 1.75f;
                shooter.bulletSpeed = 5f;
                break;

            case BossPhase.Phase3:
                movement.movementSpeed = 1.75f;
                shooter.fireRate = 1.5f;
                shooter.bulletSpeed = 5.25f;
                break;

            case BossPhase.Phase4:

                goingToCenter = true;
                
                canShoot = true;
                shooter.fireRate = 1.25f;
                shooter.bulletSpeed = 5.5f;
                animator.Play("Phase4Attack");

                break;

            case BossPhase.Phase5:
                goingToCenter = true;
                canShoot = true;
                shooter.fireRate = 0.75f;
                shooter.bulletSpeed = 6f;
                animator.Play("Phase5Attack");
              
                break;
        }
    }







}
