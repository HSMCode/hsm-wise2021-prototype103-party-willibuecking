using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoverScript : MonoBehaviour
{
    public float speed = 1f;
    private bool canMove = true;
    public GameObject cameraO;
    private Vector3 goalPos;
    private Vector3 goalPosAdder;
    private float r;
    private bool canPress = false;
    private float distanceChecker = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        r = transform.localEulerAngles.y;
        if(!Input.GetKey(KeyCode.Space) && canMove)
        {
            transform.Rotate(0.0f, 90.0f*Time.deltaTime*speed, 0.0f, Space.Self);
        }

        if((r > -3 && r < 3) | (r > 87 && r < 93) | (r > 177 && r < 183) | (r > 267 && r < 273))
        {
            canPress = true;
        }
        else
        {
            canPress = false;
        }

        if(r > -3 && r < 3)
        {
            goalPosAdder = new Vector3(-10.0f, 0f, 0.0f);
        }
        if(r > 87 && r < 93)
        {
            goalPosAdder = new Vector3(0.0f, 0f, 10.0f);
        }
        if(r > 177 && r < 183)
        {
            goalPosAdder = new Vector3(10.0f, 0f, 0f);
        }
        if(r > 267 && r < 273)
        {
            goalPosAdder = new Vector3(0.0f, 0f, -10.0f);
        }

        if(Input.GetKeyDown(KeyCode.Space) && canPress)
        {

            //Debug.Log("1");
            //Debug.Log(transform.position);
            canMove = false;
            distanceChecker = 0;
            goalPos = transform.position + goalPosAdder;
            //Debug.Log(goalPos);
        }
        if(!canMove)
        {
            //Debug.Log("2");
            GoToNextRoom();
        }
    }

    void GoToNextRoom()
    {   
        if(distanceChecker < 10)
        {   
            //Debug.Log("3");
            transform.Translate(Vector3.left* Time.deltaTime * speed*4, Space.Self);
            cameraO.transform.position = transform.position;
            distanceChecker += 1*Time.deltaTime*speed*4;
        }
        else if(distanceChecker >= 10)
        {
            //Debug.Log("4");
            //Debug.Log(transform.position);
            canMove = true;
            transform.position = goalPos;
            //Debug.Log(transform.position);
            
        }

    }

}


