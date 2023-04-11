using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public Transform Destination;
    public float Range = 1f;

    public void Update()
    {
        if (Vector3.Distance(transform.position, Player.Singleton.transform.position) < Range)
        {
            Activate(Player.Singleton.Mover);
        }
    }

    public void Activate(PlanetMover mover)
    {
        mover.Transfer(Destination);
    }
}