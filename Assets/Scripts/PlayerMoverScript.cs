using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoverScript : MonoBehaviour
{
    public float speed = 1;
    private bool canMove = true;
    public GameObject cameraO;
    private Vector3 goalPosition;
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


        if(Input.GetKeyDown(KeyCode.Space) && canPress)
        {
            canMove = false;
            distanceChecker = 0;

        }
        if(!canMove)
        {
            GoToNextRoom();
        }
    }

    void GoToNextRoom()
    {   
        if(distanceChecker < 10)
        {
            transform.Translate(Vector3.left* Time.deltaTime * speed*2, Space.Self);
            cameraO.transform.position = transform.position;
            distanceChecker += 1*Time.deltaTime*speed*2;
        }
        else
        {
            canMove = true;
        }

    }
}
