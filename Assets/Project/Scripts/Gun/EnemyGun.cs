using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : Gun
{
    private Vector3 targetPoint;

   public void SetTargetPoint(Vector3 point)
    {
        targetPoint = point;
    }

    public override void Shot()
    {
        var obj = storage.GetBullet();
        Vector3 randomPoint = targetPoint;
        randomPoint += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 2f), Random.Range(-0.5f, 0.5f));
        Vector3 targetDirection = (randomPoint - bulletSpawnPoint.position).normalized;

        obj.OnStart(parentObject, bulletSpawnPoint.position, targetDirection, speed, damage);
    }
}
