using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damageable<T>
{
    //The TakeDamage Method is called by the attacker and is resposible for damaging the attanded target
    void OnDamage(T log);
    bool IsDead();
}
