using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    private bool lightOn = false;
    public Light doorLight;
    private GameObject playerMover;
    private PlayerMoverScript playerMoverScript;

    private GameObject parentRoom;
    private RoomController roomControllerScript;

    // Start is called before the first frame update
    //private bool entranceSet = false;

    void Start()
    {
        playerMover = GameObject.Find("PlayerMover");
        playerMoverScript = playerMover.GetComponent<PlayerMoverScript>();

        parentRoom = transform.parent.gameObject;
        roomControllerScript = parentRoom.GetComponent<RoomController>();
        
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

            if(!roomControllerScript.entranceSet)
            {
                roomControllerScript.entranceID = transform.localEulerAngles.y/90f;
                roomControllerScript.entranceSet = true;
            }
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
