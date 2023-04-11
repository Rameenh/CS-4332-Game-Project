using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,90 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Astronaut")
        {
            other.GetComponent<Player>().scraps++;
            Destroy(gameObject); 
        }
    }
}
