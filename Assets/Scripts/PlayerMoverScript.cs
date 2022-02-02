using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the Rotation and movement of PlayerMover and by that the movement of the Player as well as Movement of the camera

public class PlayerMoverScript : MonoBehaviour
{
    public float speed = 2f;       //rotation-speed (also used by PlayerRotator)

    public bool canMove = true;     //flag to check if rotation is allowed (also used by PlayerRotator)

    public GameObject cameraO;      //The main Camera. Its position is also controlled by this script, it stays right above PlayerMover all the time

    private Vector3 targetPos;      //three variables used for the movement between rooms (see below for details)
    private Vector3 targetPosOffset;
    private Vector3 initialPos;

    private float tNextRoom;        //two variables used for the movement to the position needed in order to change rooms
    private float tRotator;

    private Quaternion targetRotation;      //three more variables for the rotation needed to do in order to change rooms
    private Quaternion initialRotation;
    private bool rotationFinished = false;
    
    public float goalAngle;     //a variable for LightSwitch.cs to write its rotation into
    private float targetAngle;  //a variable to store said angle here

    public bool lightTriggerOn = false;     //a flag for LightSwitch.cs to write into its Triggerstate needed as a move-condition

    private float initialAngleY;        //two variables for the rotation-process connected with room-movement
    private float interpolatedAngleY;

    private Vector3 interpolatedPosition;      //a variable used by the room-movement-rotation-function

    public bool isChangingRooms;        //a public flag to tell all other scripts (mainly RoomController.cs) that the player is currently moving between rooms, which halts some destructive operations

    // Start is called before the Yfirst frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   

        if(goalAngle == 0)                                  //define the direction of the room-changing movement depending on which LightSwitch-Trigger the Player is currently in
        {                                                   //the LightSwitches write their own rotation into goalAngle, this script then calculates if the player is facing right, left, up or down
            targetPosOffset = new Vector3(-10f, 0f, 0f);
        }
        else if(goalAngle == 90)
        {
            targetPosOffset = new Vector3(0f, 0f, 10f);
        }
        else if(goalAngle == 180)
        {
            targetPosOffset = new Vector3(10f, 0f, 0f);
        }
        else if(goalAngle == 270)
        {
            targetPosOffset = new Vector3(0f, 0f, -10f);
        }

        if(!Input.GetKey(KeyCode.Space) && canMove)     //check if the player is allowed to rotate (indicated by the canMove flag which is set to false while the player is changing between rooms) and if the space-bar is unpressed
        {
            transform.Rotate(0.0f, 80.0f*Time.deltaTime*speed, 0.0f, Space.Self);       //if yes, continuously rotate clockwise. The rotations speed is set to a fraction with a non-wholenumber denominator
                                                                                        //this is done because the music is in 120 bpm and having whole-number denominators could lead to the player not hearing sound from a room if it passes the door only on the offbeats
                                                                                        //by using ration non-whole denominators, the measure of the movement is not a subharmonic of the measure of music thus ensuring that the player passes a room on different beat-counts
            isChangingRooms = false;        //set the flag that tells other scripts that the player is chaning rooms to false. This is only important right after a room-change has been done
        }

        if(Input.GetKeyDown(KeyCode.Space) && lightTriggerOn)       //if the space bar is pressed while the PlayerBody is inside a triggerCollider of one of the LightSwitches inside a room, everything is prepared for changing rooms and roomchange is initiated, this happens as follows:
        {                                                           //the room-changing process consists of two steps:
                                                                    //1. the PlayerMovers rotation is smoothly completed to a full quarter-cycle so it is facing exactly the middle of the adjacent room and not into a wall
                                                                    //2. the PlayerMover is smoothly moved 10 measurements in the direction given by goalAngle which puts it exactly into the middle of the adjacent room it is facing towards
            targetAngle = goalAngle;            //the goalAngle is written into a separate variable that will be kept the same throughout the process, otherwise moving into the next room and entering the next rooms lightswitch-colliders would reverse the direction and make the player glitch around
            initialAngleY = transform.localEulerAngles.y;       //for the rotation, the players initialAngle at the start of the process is saved in this variable
            if(initialAngleY >= 310)        //it is important to convert rotational angles between 310 and 360 degrees to angles between -50 and 0 degrees
            {                               //otherwise, if facing almost left (with for example an angle of 355 degree), the player would be rotated backwards by the rotation-function all around the room to reach an angle of 0
                initialAngleY -= 360;       //this calculation would turn an angle of 355 degrees into -5, which in turn makes the rotation-function correctly rotate the player only 5 degrees upwards until it reaches 0
            }

            initialRotation = transform.rotation;       //the rotation of PlayerMover at KeyDown is stored here
            targetRotation = Quaternion.Euler(transform.localEulerAngles.x, targetAngle, transform.localEulerAngles.z);  //the target-rotation (calculated from targetAngle) at KeyDown is stored here

            tRotator = 0f;      //the interpolant for the rotation-Lerp isreset to 0 at keydown

            initialPos = transform.position;        //inital position at keydown is stored in this variable
            targetPos = transform.position + targetPosOffset;   //target position calculated from the targetPosOffset (see above) is stored in this variable at keydown

            tNextRoom = 0f;     //interpolant for the room-changing-movement is reset to 0

            canMove = false;        //the flag that allows continuous rotation is set to false so PlayerMover doesn´t keep on rotating while changing rooms
        }
        if (transform.rotation == targetRotation)       //check if the Player has reached target rotation or has had it already from the beginning
        {
            rotationFinished = true;                    //if yes, set the rotationfinished-flag to true, hereby stopping the rotation and allowing room-changing to start

        }

        if (transform.position == targetPos)            //check if the player has finished moving into the next room
        {
            canMove = true;     //if yes, allow PlayerMover to rotate again and
            rotationFinished = false;   //set the rotationfinished-flag to false hereby disallowing further roomchanging-movement and
            transform.position = targetPos;     //move the player precisely to the targetposition as the result of lerp is often a very tiny bit off which can lead to problems in the long run if these tiny offsets are added from multiple room changes
        }

        if(!canMove)        //if the canMove flag has been set to false
        {
            FinishRotation();       //the Lerping-process for the completion of the rotation (see below) starts and
            transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, interpolatedAngleY, transform.localEulerAngles.z); //the result of the Lerping-process is applied to PlayerMovers rotation
        }
        
         if(!canMove && rotationFinished)       //if the canMove hasn´t been reset to true yet and the rotation is finished,
        {
            GoToNextRoom();                     //start the Lerping-process of going to the next room (see below) and
            transform.position = interpolatedPosition;      //apply the result of the lerping process to the transform position
        }
    }

    void FinishRotation()       //this function calculates the rotation for the PlayerMover to finish its rotation to a full quarter-circle 
    {
        interpolatedAngleY = Mathf.Lerp(initialAngleY, targetAngle, tRotator);
        tRotator += 0.2f;
    }

    void GoToNextRoom()     //this function calculates the position of the player on its way to the next room and moves the camera
    {                     
        isChangingRooms = true;
        interpolatedPosition = Vector3.Lerp(initialPos, targetPos, tNextRoom);
        tNextRoom += 0.02f;
        cameraO.transform.position = transform.position;
    }
}


