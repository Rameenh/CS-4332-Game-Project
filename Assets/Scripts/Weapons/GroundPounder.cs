using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPounder : Weapon
{
    public GameObject Effect;

    public float MaxRadius = 5;
    public float ExpandSpeed = 1;
    

    public override void Use(Combatant user, Vector2 direction)
    {
        GameObject effect = Instantiate(Effect);
        effect.transform.position = transform.position;
        float damage = CalcHit();
        bool crit = damage != Damage;
        effect.GetComponent<GroundPounderEffect>().Fire(damage, crit, MaxRadius, ExpandSpeed);
    }
}
