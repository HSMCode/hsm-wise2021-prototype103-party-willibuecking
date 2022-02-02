using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script takes care of the lightswitches in the rooms. Each room contains for of them and they pass info to the PlayerMover 

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
        playerMoverScript = playerMover.GetComponent<PlayerMoverScript>();      //get the PlayerMover

        parentRoom = transform.parent.gameObject;
        roomControllerScript = parentRoom.GetComponent<RoomController>();       //get the room this particular switch is in
        
    }

    // Update is called once per frame
    void Update()
    {
        doorLight.enabled = lightOn;        //set the light attached to this switch to the state calculated in OnTriggerEnter below
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")       //if the PlayerBody enters the collider of this switch:
        {
            lightOn = true;     //set the lightstate-bool to on
            playerMoverScript.lightTriggerOn = true;        //tell the PlayerMoverScript that the Player is currently inside a lightswitch which means it is allowed to change rooms
            playerMoverScript.goalAngle = transform.localEulerAngles.y;     //tell the PlayerMoverScript the rotation of this particular switch which is used to calculate the direction the PlayerBody is facing

            if(!roomControllerScript.entranceSet)   //if this is the first time this switches parent-room has been entered:
            {
                roomControllerScript.entranceID = transform.localEulerAngles.y/90f;     //set the parent-rooms entrance door to the one at the position of this lightswitch and
                roomControllerScript.entranceSet = true;        //tell the parent room that an entrance has been set
            }
        }
    }

        void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")       //if the PlayerBody exits this lightswitches trigger-collider
        {
            lightOn = false;        //turn off the light
            playerMoverScript.lightTriggerOn = false;       //tell the PlayerMover taht it is no longer inside a switch meaning it can no longer change rooms
        }

    }
}
