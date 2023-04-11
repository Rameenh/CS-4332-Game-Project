using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMover : MonoBehaviour
{
    public float Size;
    public Transform ToMove;
    public Transform Planet;
    public float MoveSpeed = 5;

    void Start()
    {
        ToMove.SetParent(this.transform);
    }

    void FixedUpdate()
    {
        Move(MoveSpeed * new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")));

    }

    public void Move(Vector2 angle)
    {
        transform.Rotate(new Vector3(angle.x, 0, angle.y));
    }
}
