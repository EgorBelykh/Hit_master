using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="GunConfigDefault",menuName ="Config/Gun")]
public class GunConfig : ScriptableObject
{
    public int damage = 1;
    public int speed = 15;
    public Vector3 bulletSpawnPosition;
}
