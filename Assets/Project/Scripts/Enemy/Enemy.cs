using System;
using System.Collections;
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

   
    [SerializeField] private int health = 1;
    [SerializeField] private bool showHealthBar;

    [SerializeField] private GunConfig gunConfig;

    private EnemyGun gun = new EnemyGun();

    private Ragdoll ragdoll;
    private Animator animator;
    private HealthWidget healthWidget;
    
    private bool isBatle;
    private Coroutine gunRoutine;
    
    
    public void Initialization(BulletStorage bulletStorage)
    {
        animator = GetComponent<Animator>();
        ragdoll = GetComponent<Ragdoll>();
        ragdoll.Init();
        gun.Init(bulletStorage, gunConfig, gameObject);
        

        healthWidget = GetComponentInChildren<HealthWidget>();
        if (healthWidget)
        {
            healthWidget.Init(health);
        }
    }

    public void StartBatle(Transform playerPosition)
    {
        isBatle = true;
        if (healthWidget != null && showHealthBar) healthWidget.Show();
        //Debug.Log("Enemy Started Batle");
        
        gunRoutine = StartCoroutine(GunShot(playerPosition));
    }

    private IEnumerator GunShot(Transform playerPosition)
    {
        float timer = 0;

        yield return new WaitForSeconds(UnityEngine.Random.Range(1f,2.5f));

        while (isBatle)
        {
            if (timer == 0)
            {
                timer = 10;
                gun.SetTargetPoint(playerPosition.position);
                gun.Shot();
            }
            else
            {
                timer -= Time.deltaTime;
                timer = Mathf.Clamp(timer, 0, 10);
            }

            yield return null;
        }
        yield break;
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
        StopCoroutine(gunRoutine);
        Destroy(gameObject, 2f);

    }
}
