using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float Damage;
    public float CritChance;
    public float CritMultiplier;

    public abstract void Use(Combatant user, Vector2 direction);
    public float CalcHit()
    {
        //Critchance probability of a critical hit, which multiplies the damage by CritMultiplier
        bool crit = Random.Range(0f, 1f) < CritChance;
        return Damage * (crit ? CritMultiplier : 1);
    }
    public void DealDamage(Combatant user, Combatant target)
    {
        float hitDamage = CalcHit();
        //if this hits damage is different than the normal amount of damage, it was a crit
        user.DealDamage(hitDamage, target, hitDamage != Damage);
    }
}
