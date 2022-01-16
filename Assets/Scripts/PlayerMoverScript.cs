using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoverScript : MonoBehaviour
{
    public float speed = 1f;
    public bool canMove = true;
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

        if(r > -10 && r < 10)
        {
            goalPosAdder = new Vector3(-10.0f, 0f, 0.0f);
        }
        if(r > 80 && r < 100)
        {
            goalPosAdder = new Vector3(0.0f, 0f, 10.0f);
        }
        if(r > 170 && r > -170)
        {
            goalPosAdder = new Vector3(10.0f, 0f, 0f);
        }
        if(r > -80 && r > -100)
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

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LightSwitch")
        {
            canPress = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "LightSwitch")
        {
            canPress = false;
        }
    }

}


