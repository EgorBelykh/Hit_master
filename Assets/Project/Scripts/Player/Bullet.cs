using System;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public event Action<Bullet, Collider> onBulletCollisionEvent;
    public event Action<Bullet> onBulletTimeOutEvent;

    private int speed;
    public int damage { get; private set; }
    private Vector3 direction;
    private bool isMove;
    private float timer;
    private GameObject parentObject;

    public void OnStart(GameObject obj, Vector3 point, Vector3 direction, int speed, int damage)
    {
        isMove = true;
        SetPosition(point);
        SetDirection(direction);
        SetSpeed(speed);
        SetDamage(damage);
        parentObject = obj;
        Debug.Log(obj);
        Show();
    }

    public GameObject GetParentObject()
    {
        return parentObject;
    }

    public void OnStop()
    {
        Hide();
        isMove = false;

    }

    public void SetPosition(Vector3 point)
    {
        transform.position = point;
    }
    
    public void SetDirection(Vector3 dir)
    {
        direction = dir;
        direction.Normalize();
    }

    public void SetSpeed(int value)
    {
        speed = value;
    }

    public void SetDamage(int value)
    {
        damage = value;
    }

    

    private void FixedUpdate()
    {
        if (isMove)
        {
            var position = transform.position;
            
            position += direction * speed * Time.fixedDeltaTime;
            transform.position = position;
            timer += Time.deltaTime;
            if (timer >= 5f)
            {
                timer = 0;
                onBulletTimeOutEvent?.Invoke(this);
                OnStop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        OnStop();
        //Debug.Log($"{other.gameObject.name}");
        onBulletCollisionEvent?.Invoke(this, other);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }


}
