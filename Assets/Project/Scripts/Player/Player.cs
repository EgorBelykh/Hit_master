using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator),typeof(NavMeshAgent))]
public class Player : MonoBehaviour, IDamageble
{
    public event Action onPathCompleteEvent;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Gun gun = new Gun();

    private bool isWay = false;
    private bool isBatle;

    public void Initialization(Vector3 point)
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        var obj = new GameObject("Bullet Pool");
        var bulletStorage = obj.AddComponent<BulletStorage>();
        gun.Init(bulletStorage);
        transform.position = point;
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
                Debug.Log($"{navMeshAgent.destination}");
                if (distance <= navMeshAgent.stoppingDistance)
                {
                    isWay = false;
                    Debug.Log("PathComplete");
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
        
    }
}
