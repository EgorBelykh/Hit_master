using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerGun : Gun
{

    public override void Shot()
    {
        var camera = Camera.main;

        var mousePosition = Input.mousePosition;
        var ray = camera.ScreenPointToRay(mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100);

        RaycastHit hit;
        var obj = storage.GetBullet();

        if (Physics.Raycast(ray, out hit))
        {
            float distance = Vector3.Distance(ray.origin, hit.point);

            obj.OnStart(parentObject, bulletSpawnPoint.position, hit.point - bulletSpawnPoint.position, speed, damage);
            Debug.DrawRay(bulletSpawnPoint.position, ray.GetPoint(distance), Color.blue);
        }
        else
        {
            obj.OnStart(parentObject, bulletSpawnPoint.position, ray.GetPoint(100) - bulletSpawnPoint.position, speed, damage);
            Debug.DrawRay(bulletSpawnPoint.position, ray.GetPoint(100), Color.red);
        }



    }
}
