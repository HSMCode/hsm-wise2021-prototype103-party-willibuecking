using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    private bool lightOn = false;
    public Light doorLight;
    private GameObject playerMover;
    private PlayerMoverScript playerMoverScript;
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerMover = GameObject.Find("PlayerMover");
        playerMoverScript = playerMover.GetComponent<PlayerMoverScript>();
        
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
            playerMoverScript.lightTriggerOn = true;
            playerMoverScript.goalAngle = transform.localEulerAngles.y;
            //Debug.Log(transform.localEulerAngles.y);
        }
    }

        void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            lightOn = false;
            playerMoverScript.lightTriggerOn = false;
        }
    }
}
