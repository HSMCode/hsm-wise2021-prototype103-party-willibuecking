using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script rotates the Players body against the direction that PlayerMover.cs rotates the PlayerBodys parent
//this is done because the audiolistener is attached to the players body and the original rotation of PlayerMover makes the sounds coming from the passed rooms move in the panoramic field additional to the changes in volume
//this change in direction can be disrupting as the camrea doesn´t rotate with the player so the sound seems to change position making it very hard to locate

public class PlayerRotatorScript : MonoBehaviour
{
    private GameObject playerMover;     //GameObject variable for the PlayerMover 
    private PlayerMoverScript playerMoverScript;        //variable for the script rotating PlayerMover
    private bool canRotate;     //bool to see if the player is rotating
    private float speed;        //speed used before for balancing and kept in
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerMover = GameObject.Find("PlayerMover");            //assign the PlayerMover to the corresponding variable
        playerMoverScript = playerMover.GetComponent<PlayerMoverScript>();  //assign the PlayerMoverScript to the corresponding variable
    }

    // Update is called once per frame
    void Update()
    {
        canRotate = playerMoverScript.canMove;                              //read the value of canRotate inside PlayerMoverScript.cs and assign it to the corresponding variable here so the player counters only actually existing rotation
        speed = playerMoverScript.speed;            //get the speed of PlayerMoverScript.cs so the speeds of both rotations match
        if(!Input.GetKey(KeyCode.Space) && canRotate)       //this is the same condition used for doing the rotation of PlayerMover, if they are met:
        {
            transform.Rotate(0.0f, -80.0f*Time.deltaTime*speed, 0.0f, Space.Self);      //the PlayerBody rotates in the opposite direction thus countering the rotation imposed by PlayerMover and keeping its rotation practically unchanged
        }
    }
}
