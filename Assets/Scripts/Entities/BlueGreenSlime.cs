using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGreenSlime : EnemyFollower
{
    public int upCounter;
    public int downCounter;
    public int leftCounter;
    public int rightCounter;
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
                if (upCounter != 2)
                {
                    mover.Move(new Vector2(MoveSpeed, 0));
                    upCounter++;
                    downCounter--;
                }
                else //moving down
                {
                    mover.Move(new Vector2(-1 * MoveSpeed, 0));
                    downCounter++;
                    upCounter--;
                }
            }
            else if (randomDir == 1)  //moving down
            {
                if (downCounter != 2)
                {
                    mover.Move(new Vector2(-1 * MoveSpeed, 0));
                    downCounter++;
                    upCounter--;
                }
                else  //moving up
                {
                    mover.Move(new Vector2(MoveSpeed, 0));
                    upCounter++;
                    downCounter--;
                }
            }
            else if (randomDir == 2) //moving left
            {
                if (leftCounter != 2)
                {
                    mover.Move(new Vector2(0, -1 * MoveSpeed));
                    leftCounter++;
                    rightCounter--;
                }
                else  //moving right
                {
                    mover.Move(new Vector2(0, MoveSpeed));
                    rightCounter++;
                    leftCounter--;
                }
            }
            else if(randomDir == 3) //moving right
            {
                if (rightCounter != 2)
                {
                    mover.Move(new Vector2(0, MoveSpeed));
                    rightCounter++;
                    leftCounter--;
                }
                else //moving left
                {
                    mover.Move(new Vector2(0, -1 * MoveSpeed));
                    leftCounter++;
                    rightCounter--;
                }
            }
        }
    }
}
