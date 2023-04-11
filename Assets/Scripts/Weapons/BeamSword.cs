using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BeamSword : Weapon
{
    private Animator anim;
    private Combatant user;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }
    public override void Use(Combatant user, Vector2 direction)
    {
        this.user = user;
        anim.SetTrigger("Swing");
    }
    public void OnTriggerEnter(Collider other)
    {
        Combatant target = other.GetComponent<Combatant>();
        if (target == null || user == null || target.Team == user.Team) return;
        DealDamage(user, target);
    }
}
