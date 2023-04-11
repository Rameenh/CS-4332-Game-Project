using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollower : Combatant
{
    public GameObject Mover;
    protected PlanetMover mover;
    public float MoveSpeed = 0.25f;
    public float Damage = 1;
    public int BeatsPerMove = 1;

    public bool touchingPlayer = false;

    public override void Start()
    {
        Team = Team.Enemy;
        mover = Mover.GetComponent<PlanetMover>();
        base.Start();
    }

    public override void Die()
    {
        base.Die();
        Destroy(Mover); //mover is my parent
    }

    public override void OnBeat(int beatNo)
    {
        if (Player.Singleton.Mover.Planet != mover.Planet)
        {
            return;
        }
        if (touchingPlayer)
        {
            DealDamage(Damage, Player.Singleton, false);
        }
        if (beatNo % BeatsPerMove == 0)
        {
            //player and enemy are on the same planet, so enemy moves toward player
            Quaternion newrot = Quaternion.RotateTowards(mover.transform.rotation, Player.Singleton.Mover.transform.rotation, mover.Arclength2Deg(MoveSpeed));
            mover.SetTargetRotation(newrot);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            touchingPlayer = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            touchingPlayer = false;
        }
    }
}
