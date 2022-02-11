using System;
using System.Collections.Generic;
using UnityEngine;

public class WayHandler : MonoBehaviour
{

    public event Action onWayFinishedEvent;

    [SerializeField]
    private WayPoint[] wayPoints;

    private int pointIndex = 0;
    

    public void Initialization()
    {
        foreach (var item in wayPoints)
        {
            if (item.enemies.Length > 0)
            {
                foreach (var enemy in item.enemies)
                {
                    enemy.Initialization();
                }
            }
        }
    }


    public Vector3 GetNextPoint()
    {
        if (pointIndex < wayPoints.Length - 1)
        {
            pointIndex++;
            return wayPoints[pointIndex].point.position;
        }
        else if (pointIndex == wayPoints.Length -1)
        {
            onWayFinishedEvent?.Invoke();
        }

        return wayPoints[0].point.position;
    }
    public Vector3 GetPointAt(int index)
    {
        return wayPoints[index].point.position;
    }

    public bool GetIsBatle()
    {
        return wayPoints[pointIndex].isBatle;
    }

    public Enemy[] GetEnemies()
    {
        return wayPoints[pointIndex].enemies;
    }

    public void Restart()
    {
        pointIndex = 0;
    }

    

}
