using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy
{
    public float _health = 100.0f;
    
    
    public void TakeDamage(float bulletDamage)
    {
        _health -= bulletDamage;
    }
}
