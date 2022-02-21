using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator),typeof(NavMeshAgent))]
public class Player : MonoBehaviour, IDamageble
{
    public event Action onPathCompleteEvent;
    public event Action onDiedEvent;

    [SerializeField] private int health = 10;
    [SerializeField] private bool showHealthBar;
    [SerializeField] private GunConfig gunConfig;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Gun gun = new PlayerGun();
    private HealthWidget healthWidget;
    private bool isWay = false;
    private bool isBatle;

    public void Initialization(Vector3 point, BulletStorage bulletStorage)
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        gun.Init(bulletStorage,gunConfig,gameObject);
        transform.position = point;

        healthWidget = GetComponentInChildren<HealthWidget>();
        if (healthWidget != null)
        {
            healthWidget.Init(health);
            if (showHealthBar)
            {
                healthWidget.Show();
            }
           
        }
    }

    public void BatleStart()
    {
        isBatle = true;
    }

    public void BatleStop()
    {
        isBatle = false;
    }

    private void Update()
    {
        if (isWay)
        {
            if (true)
            {
                var distance = (transform.position - navMeshAgent.destination).magnitude;
                //Debug.Log($"{navMeshAgent.destination}");
                if (distance <= navMeshAgent.stoppingDistance)
                {
                    isWay = false;
                    //Debug.Log("PathComplete");
                    animator.SetBool("IsWay", false);
                    onPathCompleteEvent?.Invoke();

                }
            }
            
        }

    }



    public void Click()
    {
        if (isBatle)
        {
            gun.Shot();
        }
    }

    public void SetDistanation(Vector3 point)
    {
        Debug.Log("SetPath");
        navMeshAgent.SetDestination(point);
        animator.SetBool("IsWay", true);
        isWay = true; 
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (healthWidget != null && showHealthBar) healthWidget.ChangeHealth(health);
        if (health <= 0)
        {
            Died();
        }
    }

    private void Died()
    {
        onDiedEvent?.Invoke();
    }
}
