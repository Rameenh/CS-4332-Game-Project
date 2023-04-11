using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Combatant
{
    private Vector3 velocity;
    private float damage;
    private bool crit = false;
    private float lifetime;
    private int pierce;
    private Transform planet;
    private float planetRadius;

    public void Fire(Transform user, Transform planet, float planetRadius, float speed, float damage, bool crit, float lifetime, int pierce)
    {
        this.damage = damage;
        this.crit = crit;
        this.lifetime = lifetime;
        this.pierce = pierce;
        this.planet = planet;
        this.planetRadius = planetRadius;
        transform.position = user.transform.position;
        transform.rotation = user.transform.rotation;
        velocity = -user.up*speed;
    }

    void Update()
    {
        transform.position += velocity*Time.deltaTime;
        //uniform circular motion
        velocity += Time.deltaTime*(velocity.sqrMagnitude/planetRadius)*(planet.transform.position - transform.position).normalized;
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Die();
        }
    }


    void OnTriggerEnter(Collider collider)
    {
        Combatant other = collider.GetComponent<Combatant>();
        if (other == null || other.Team == Team)
            return;
        DealDamage(damage, other, crit);
        if (--pierce == 0)
        {
            Die();
        }
    }
    public override void Die()
    {
        Destroy(gameObject);
    }
}
