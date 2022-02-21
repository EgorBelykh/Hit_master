using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Gun
{
    protected int damage = 1;
    protected int speed = 15;
    protected Transform bulletSpawnPoint;
    protected BulletStorage storage;
    protected GameObject parentObject;

    public virtual void Init(BulletStorage bulletStorage, GunConfig config,GameObject parent)
    {
        damage = config.damage;
        speed = config.speed;
        bulletSpawnPoint = new GameObject("BulletSpawnPoint").transform;
        bulletSpawnPoint.parent = parent.transform;
        bulletSpawnPoint.localPosition = config.bulletSpawnPosition;
        storage = bulletStorage;
        storage.Init();
    }

    public abstract void Shot();
    
  
}
