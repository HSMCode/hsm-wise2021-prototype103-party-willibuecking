using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    private bool lightOn = false;
    public Light doorLight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        doorLight.enabled = lightOn;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            lightOn = true;
        }
    }

        void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            lightOn = false;
        }
    }
}
