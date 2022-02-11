using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    
    private Rigidbody[] rigidbodies;

    public void Init()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        SetRagdollKinematick(true);
    }

    public void SetRagdollKinematick(bool value)
    {
        foreach (var item in rigidbodies)
        {
            item.isKinematic = value;
        }
    }
}
