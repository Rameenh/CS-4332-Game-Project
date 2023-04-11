using System.Collections;
using UnityEngine;

public class GroundPounderEffect : Combatant
{
    public float MaxRadius = 5;
    public float ExpandSpeed = 1;
    private float progress = 0;

    private float damage;
    private bool crit = false;

    public void Fire(float damage, bool crit, float maxRadius, float expandSpeed, Team team = Team.Player)
    {
        this.damage = damage;
        this.crit = crit;
        this.Team = team;
        this.MaxRadius = maxRadius;
        this.ExpandSpeed = expandSpeed;
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (progress < 1) {
            //expand sphere in all directions
            transform.localScale = Vector3.one*Mathf.Lerp(0, MaxRadius, progress);
            progress += ExpandSpeed*Time.deltaTime/MaxRadius;
            yield return null;
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        Combatant other = collider.GetComponent<Combatant>();
        if (other == null || other.Team == Team)
            return;
        DealDamage(damage, other, crit);
    }
}
