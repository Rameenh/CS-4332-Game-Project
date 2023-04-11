using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMover : MonoBehaviour
{
    public Transform ToMove;
    public Transform Planet;
    public float SmoothSpeed = 5;
    public float Gravity = 0.1f;

    private Quaternion targetRotation;
    private Quaternion toMoveTargetRotation;
    public float planetSize { get; protected set; }
    public float fallSpeed { get; protected set; }

    void Start()
    {
        //make sure the object we're moving is a parent to this gameobject (since we move with rotation)
        ToMove.SetParent(this.transform);
        //set the position of ToMove to the top of the planet
        Transfer(Planet);
    }

    void Update()
    {
        //smoothly move towards the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * SmoothSpeed);
        ToMove.localRotation = Quaternion.Lerp(ToMove.transform.localRotation, toMoveTargetRotation, Time.deltaTime * SmoothSpeed);
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(ToMove.position, transform.position) <= planetSize)
        {
            //move player to surface of planet
            if (ToMove.localPosition == Vector3.zero)
            {
                //make sure to take care of case where player is in exact middle of planet (can happen on scene load)
                ToMove.localPosition = Vector3.up * (planetSize - 0.0001f);
            }
            else
            {
                ToMove.localPosition = ToMove.localPosition.normalized * (planetSize - 0.0001f);
            }

            if (fallSpeed != 0)
            {
                //just landed, so reset
                reset();
            }
            fallSpeed = 0;
        }
        else
        {
            //fall towards target planet
            fallSpeed += Gravity;
            ToMove.position += (transform.position - ToMove.position).normalized * fallSpeed;
        }
    }

    //moves on the surface of the planet arclength units
    public void Move(Vector2 arclength)
    {
        //rotate angle.x degrees around the x axis, then angle.y degrees around the y axis
        targetRotation = transform.rotation * Quaternion.AngleAxis(Mathf.Rad2Deg * arclength.x / planetSize, Vector3.right) * Quaternion.AngleAxis(Mathf.Rad2Deg * arclength.y / planetSize, Vector3.up);
        Debug.Log($"{gameObject.name} rotation {targetRotation.eulerAngles}");
    }
    public void SetTargetRotation(Quaternion target)
    {
        targetRotation = target;
    }

    public void Transfer(Transform toPlanet)
    {
        Planet = toPlanet;
        reset();
        //todo: player rotation
    }

    public void CopyOrientation(PlanetMover from)
    {
        //copy planet mover
        transform.parent = from.transform.parent;
        transform.position = from.transform.position;
        transform.rotation = from.transform.rotation;
        planetSize = from.planetSize;
        Planet = from.Planet;
        //copy child
        ToMove.transform.SetParent(transform);
        ToMove.transform.position = from.ToMove.transform.position;
        ToMove.transform.rotation = from.ToMove.transform.rotation;
        toMoveTargetRotation = from.toMoveTargetRotation;
        targetRotation = from.targetRotation;
    }

    public float Deg2Arclength(float degrees) => Mathf.Deg2Rad * degrees * planetSize;
    public float Arclength2Deg(float arclength) => Mathf.Rad2Deg * arclength / planetSize;

    private float calcPlanetRadius() => (Planet.localScale.y + ToMove.localScale.y) / 2;

    private void reset()
    {
        //reset calculated components, move to the center of Planet, reset rotation, reset fall speed
        //do this all while unchilding ToMove so that their position is unchanged.
        planetSize = calcPlanetRadius();
        ToMove.transform.SetParent(null, true);
        transform.position = Planet.transform.position;
        fallSpeed = 0;
        toMoveTargetRotation = Quaternion.identity;
        transform.LookAt(ToMove);
        ToMove.transform.SetParent(transform, true);
        targetRotation = transform.rotation;
    }
}
