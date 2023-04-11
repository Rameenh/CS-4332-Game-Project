using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float DistFromPlanetSurface = 10;
    public float DistBehindPlayer = 10;
    public float PitchOffset = 10;
    public float SmoothSpeed = 3;
    public Transform Following;
    public PlanetMover FollowingParent;

    void Update()
    {
        if (FollowingParent == null)
        {
            Debug.LogError("CameraMover following parent null!");
            return;
        }
        Vector3 abovePlanetPos = FollowingParent.transform.position + (Following.transform.position - FollowingParent.transform.position).normalized * (FollowingParent.planetSize + DistFromPlanetSurface);
        Vector3 behindPlayerPos = abovePlanetPos - Following.transform.forward * DistBehindPlayer;

        transform.position = Vector3.Lerp(transform.position, behindPlayerPos, SmoothSpeed * Time.deltaTime);

        Quaternion old = transform.rotation;
        transform.LookAt(Following, Following.forward);
        transform.Rotate(new Vector3(PitchOffset, 0, 0), Space.Self);
        transform.rotation = Quaternion.Slerp(old, transform.rotation, Time.deltaTime * SmoothSpeed);
    }
}
