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

    // Start is called before the first frame update
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
            transform.Rotate(0.0f, 90.0f*Time.deltaTime*speed, 0.0f, Space.Self);
        }

        if(Input.GetKeyDown(KeyCode.Space) && lightTriggerOn)
        {
            //Debug.Log("1");
            targetAngle = goalAngle;
            targetRotation = Quaternion.Euler(transform.localEulerAngles.x, goalAngle, transform.localEulerAngles.z);
            initialRotation = transform.rotation;
            tRotator = 0f;
            Debug.Log(targetRotation);
            Debug.Log(initialRotation);
            targetPos = transform.position + targetPosOffset;
            Debug.Log(targetPos);
            initialPos = transform.position;
            Debug.Log(initialPos);
            canMove = false;
            tNextRoom = 0f;
            //Debug.Log("2");
        }
        /* if (transform.rotation == targetRotation)
        {
            rotationFinished = true;
            //Debug.Log("3");
        } */

        if (transform.position == targetPos)
        {
            canMove = true;
            rotationFinished = false;
            //Debug.Log("4");
        }

        if(!canMove)
        {
            //Debug.Log("5");
            FinishRotation();
        }
        
         if(!canMove && rotationFinished)
        {
            //Debug.Log("6");
            GoToNextRoom();
        }
    }


    /* void FinishRotation()
    {
        transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, tRotator);
        tRotator += 0.05f;
        //Debug.Log("7");

    } */

    void GoToNextRoom()
    {
        transform.position = Vector3.Lerp(initialPos, targetPos, tNextRoom);
        tNextRoom += 0.005f;
        cameraO.transform.position = transform.position;
        //Debug.Log("8");
    }
}


