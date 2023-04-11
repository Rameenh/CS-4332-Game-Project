using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupUI : MonoBehaviour
{
    public GameObject DoubleSpeedBeatLine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Singleton.rateOfFire == 2) {
            DoubleSpeedBeatLine.SetActive(true);
        } else {
            DoubleSpeedBeatLine.SetActive(false);
        }
    }
}
