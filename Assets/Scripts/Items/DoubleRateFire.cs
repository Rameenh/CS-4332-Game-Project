using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleRateFire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update(){
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

        private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Astronaut")
        {
            other.GetComponent<Player>().tempDoubleRateOfFire();
            Destroy(gameObject);
        }
    }
}
