using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BeatIndicator : MonoBehaviour
{
    public GameObject BeatIndicatorLine;
    public GameObject BeatIndicatorBackground;
    private float BeatIndicatorBackgroundLeftEdge;
    private float BeatIndicatorBackgroundRightEdge;

    void Start()
    {
        BeatIndicatorLine = GameObject.Find("BeatIndicatorLine"); //hooks up the beat indicator line to this script so we can move it
        BeatIndicatorLine.SetActive(true); //line should be hidden in the beginning of the level
        BeatIndicatorBackground = GameObject.Find("BeatIndicatorBackground"); //same as above but for the background

        BeatIndicatorBackgroundLeftEdge = 0 - (BeatIndicatorBackground.GetComponent<RectTransform>().rect.width); //defines left edge of the beat indicator background
        BeatIndicatorBackgroundRightEdge = 0 + (BeatIndicatorBackground.GetComponent<RectTransform>().rect.width) / 2; //defines right edge of the beat indicator background

        BeatIndicatorLine.transform.localPosition = new Vector3(BeatIndicatorBackgroundRightEdge, 0, 0); //starts beat indicator line in rightmost position

    }

    void Update() //have line spawn on right edge if beat is within 1 second of occuring
    {
        if(!(PauseMenu.GameIsPaused)){
            BeatIndicatorLine.transform.localPosition = new Vector3(BeatIndicatorBackgroundLeftEdge * (Beat.Singleton.sectionBeatPosition - (float)Math.Truncate(Beat.Singleton.sectionBeatPosition)) + BeatIndicatorBackgroundRightEdge, 0, 0); //should move the line leftward
        }

    }
}
