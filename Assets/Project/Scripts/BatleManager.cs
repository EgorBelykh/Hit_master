using System;
using System.Collections.Generic;
using UnityEngine;

public class BatleManager
{

    public event Action onBatleCompleteEvent;

    private List<Enemy> enemies = new List<Enemy>();

    public void StartBatle(Enemy[] objs)
    {
        Debug.Log("Batle start");
        foreach (var item in objs)
        {
            item.onDiedEvent += DiedEnemy;
            item.StartBatle();
            enemies.Add(item);
        }
    }

    public void DiedEnemy(Enemy enemy)
    {
        enemy.onDiedEvent -= DiedEnemy;
        enemies.Remove(enemy);
        if (enemies.Count == 0)
        {
            onBatleCompleteEvent?.Invoke();
            Debug.Log("Batle Complete");
        }
    }
}
