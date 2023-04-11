using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    public float MaxHealth;
    public float Health;
    public Team Team;
    public GameObject DamageNumberPrefab;

    public bool invincibility = false;

    public virtual void Start()
    {
        Beat.OnBeatEnter += OnBeat;
    }
    public virtual void TakeDamage(float amount, bool wasCrit = false)
    {
        if(!invincibility){
            Health -= amount;
            GameObject damageNumber = Instantiate(DamageNumberPrefab);
            damageNumber.GetComponent<DamageNumber>().Display(this, amount, wasCrit, Health <= 0);
            if (Health <= 0)
            {
                Health = 0;
                Die();
            }
        }
        else{
            invincibility = false;
        }
    }
    public virtual void DealDamage(float amount, Combatant target, bool wasCrit = false)
    {
        if (target.Team != Team)
        {
            target.TakeDamage(amount, wasCrit);
        }
    }
    public virtual void Die()
    {
        Beat.OnBeatEnter -= OnBeat;
    }
    public virtual void OnBeat(int beatno)
    {
        
    }
}
