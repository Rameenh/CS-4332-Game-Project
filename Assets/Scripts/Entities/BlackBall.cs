using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBall : EnemyFollower
{
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
            int randomDir = Random.Range(0, 3);
            if (randomDir == 0) //moving up
            {
                mover.Move(new Vector2(MoveSpeed, 0));
            }
            else if (randomDir == 1)  //moving down
            {
                mover.Move(new Vector2(-1 * MoveSpeed, 0));
            }
            else if (randomDir == 2) //moving left
            {
                mover.Move(new Vector2(0, -1 * MoveSpeed));
            }
            else if(randomDir == 3) //moving right
            {
                mover.Move(new Vector2(0, MoveSpeed));
            }
        }
    }
}
