using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledMover : MonoBehaviour
{
    public PlanetMover Mover;
    public float MoveSpeed = 0.5f;
    public Transform Planet;

    private bool movedThisBeat = false; //have I moved on this beat?

    private void resetMovedThisBeat(int beat) => movedThisBeat = false;

    void Awake()
    {
        Mover.Planet = Planet;
    }

    void Start()
    {
        Beat.OnBeatEnter += resetMovedThisBeat;
    }

    void OnDestroy()
    {
        Beat.OnBeatEnter -= resetMovedThisBeat;
    }

    // Update is called once per frame
    void Update()
    {

        //grounded and on beat
        if (!movedThisBeat && Beat.Singleton.InBeat() && Mover.fallSpeed == 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Mover.Move(new Vector2(MoveSpeed, 0));
                movedThisBeat = true;
            }
            else if (!movedThisBeat && Input.GetKeyDown(KeyCode.S))
            {
                Mover.Move(new Vector2(-1 * MoveSpeed, 0));
                movedThisBeat = true;
            }
            else if (!movedThisBeat && Input.GetKeyDown(KeyCode.A))
            {
                Mover.Move(new Vector2(0, -1 * MoveSpeed));
                movedThisBeat = true;
            }
            else if (!movedThisBeat && Input.GetKeyDown(KeyCode.D))
            {
                Mover.Move(new Vector2(0, MoveSpeed));
                movedThisBeat = true;
            }
        }
    }
    
}
