using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletStorage : MonoBehaviour
{
    private List<Bullet> activeBullets = new List<Bullet>();
    private List<Bullet> disactiveBullets = new List<Bullet>();

    private int bulletCount  = 50;

    public void Init()
    {
        Bullet prefab = Resources.Load<Bullet>("Prefabs/Bullet");
        
        for (int i = 0; i < bulletCount; i++)
        {
            var obj = Instantiate(prefab, transform);
            obj.Hide();
            disactiveBullets.Add(obj);

        }

    }

    public Bullet GetBullet()
    {
        var obj = disactiveBullets[0];
        disactiveBullets.Remove(obj);
        activeBullets.Add(obj);
        obj.onBulletCollisionEvent += BulletCollision;
        obj.onBulletTimeOutEvent += BulletTimeOut;
        return obj;
    }

    public void DisableBullet(Bullet bullet)
    {
        bullet.onBulletCollisionEvent -= BulletCollision;
        bullet.onBulletTimeOutEvent -= BulletTimeOut;
        activeBullets.Remove(bullet);
        disactiveBullets.Add(bullet);
    }

    public void BulletCollision(Bullet bullet, Collider collider)
    {
        IDamageble obj = collider.gameObject.GetComponent<IDamageble>();
        if (obj != null)
        {
            obj.TakeDamage(bullet.damage);
        }

        DisableBullet(bullet);
    }

    public void BulletTimeOut(Bullet bullet)
    {
        DisableBullet(bullet);
    }
}
