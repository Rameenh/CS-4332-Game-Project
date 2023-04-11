using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public GameObject BulletPrefab;
    private Combatant user;
    public float BulletSpeed = 5;
    public float BulletLifetime = 3;
    public int BulletPierce = 1;

    public override void Use(Combatant user, Vector2 direction)
    {
        this.user = user;
        GameObject mover = Instantiate(BulletPrefab);
        Bullet bullet = mover.GetComponent<Bullet>();
        bullet.Team = user.Team;
        float damage = CalcHit();
        bool crit = damage != Damage;
        var userPlanetMover = user.transform.parent.GetComponent<PlanetMover>(); 
        bullet.Fire(user.transform, userPlanetMover.Planet, userPlanetMover.planetSize,
                BulletSpeed, damage, crit, BulletLifetime, BulletPierce);
    }
}
