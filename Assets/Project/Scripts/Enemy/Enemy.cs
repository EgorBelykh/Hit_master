using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(
    typeof(NavMeshAgent),
    typeof(Animator),
    typeof(Ragdoll)
    )]
public class Enemy : MonoBehaviour, IDamageble
{

    public event Action<Enemy> onDiedEvent;

    [SerializeField] private int damage = 1;
    [SerializeField] private int health = 1;
    [SerializeField] private bool showHealthBar;

    private Ragdoll ragdoll;
    private Animator animator;
    private HealthWidget healthWidget;

    private bool isBatle;
    
    
    public void Initialization()
    {
        animator = GetComponent<Animator>();
        ragdoll = GetComponent<Ragdoll>();
        ragdoll.Init();
        healthWidget = GetComponentInChildren<HealthWidget>();
        if (healthWidget)
        {
            healthWidget.Init(health);
        }
    }

    public void StartBatle()
    {
        isBatle = true;
        if (healthWidget != null && showHealthBar) healthWidget.Show();
        //Debug.Log("Enemy Started Batle");
    }

    public void TakeDamage(int damage)
    {
        if (isBatle == false) return;
        health -= damage;
        if(healthWidget != null && showHealthBar) healthWidget.ChangeHealth(health);
        if (health <= 0)
        {
            Died();
        }
    }

    public void Died()
    {
        onDiedEvent?.Invoke(this);
        animator.enabled = false;
        if (healthWidget != null && showHealthBar) healthWidget.Hide();
        ragdoll.SetRagdollKinematick(false);
        isBatle = false;
        Destroy(gameObject, 2f);

    }
}
