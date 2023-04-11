using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSlime : EnemyFollower
{
    public int alternator = 1;
    public bool isVerticalMoving = true;
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
            if (isVerticalMoving)
            {
                if (alternator > 0)
                {
                    mover.Move(new Vector2(MoveSpeed, 0));
                }
                else
                {
                    mover.Move(new Vector2(-1 * MoveSpeed, 0));
                }
            }
            else
            {
                if (alternator > 0)
                {
                    mover.Move(new Vector2(0, -1 * MoveSpeed));
                }
                else
                {
                    mover.Move(new Vector2(0, MoveSpeed));
                }
            }

            alternator *= -1;
        }
    }
}
