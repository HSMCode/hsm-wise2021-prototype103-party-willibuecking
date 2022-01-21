using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoverScript : MonoBehaviour
{
    public float speed = 1f;

    public bool canMove = true;

    public GameObject cameraO;

    private Vector3 targetPos;
    private Vector3 targetPosOffset;
    private Vector3 initialPos;

    private float tNextRoom;
    private float tRotator;

    private Quaternion targetRotation;
    private Quaternion initialRotation;
    private bool rotationFinished = false;
    
    public float goalAngle;
    private float targetAngle;
    public bool lightTriggerOn = false;

    private float initialAngleY;
    private float interpolatedAngleY;

    private Vector3 interpolatedPosition;


    public bool isChangingRooms;

    // Start is called before the Yfirst frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   

        if(goalAngle == 0)
        {
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

        if(!Input.GetKey(KeyCode.Space) && canMove)
        {
            transform.Rotate(0.0f, 80.0f*Time.deltaTime*speed, 0.0f, Space.Self);
            isChangingRooms = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && lightTriggerOn)
        {
            targetAngle = goalAngle;
            initialAngleY = transform.localEulerAngles.y;
            if(initialAngleY >= 310)
            {
                initialAngleY -= 360;
            }

            initialRotation = transform.rotation;
            targetRotation = Quaternion.Euler(transform.localEulerAngles.x, targetAngle, transform.localEulerAngles.z);

            tRotator = 0f;

            initialPos = transform.position;
            targetPos = transform.position + targetPosOffset;

            tNextRoom = 0f;

            canMove = false;
        }
        if (transform.rotation == targetRotation)
        {
            rotationFinished = true;

        }

        if (transform.position == targetPos)
        {
            canMove = true;
            rotationFinished = false;
            transform.position = targetPos;
        }

        if(!canMove)
        {
            FinishRotation();
            transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, interpolatedAngleY, transform.localEulerAngles.z);
        }
        
         if(!canMove && rotationFinished)
        {
            GoToNextRoom();
            transform.position = interpolatedPosition;
        }
    }

    void FinishRotation()
    {
        interpolatedAngleY = Mathf.Lerp(initialAngleY, targetAngle, tRotator);
        tRotator += 0.05f;
    }

    /* void FinishRotation()
    {
        transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, tRotator);
        tRotator += 0.05f;
        //Debug.Log("7");

    } */

    void GoToNextRoom()
    {
        isChangingRooms = true;
        interpolatedPosition = Vector3.Lerp(initialPos, targetPos, tNextRoom);
        tNextRoom += 0.005f;
        cameraO.transform.position = transform.position;
    }
}


