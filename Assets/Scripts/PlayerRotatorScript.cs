using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotatorScript : MonoBehaviour
{
    private GameObject playerMover;
    private PlayerMoverScript playerMoverScript;
    private bool canRotate;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerMover = GameObject.Find("PlayerMover");
        playerMoverScript = playerMover.GetComponent<PlayerMoverScript>();
        canRotate = playerMoverScript.canMove;
        speed = playerMoverScript.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetKey(KeyCode.Space) && canRotate)
        {
            transform.Rotate(0.0f, -90.0f*Time.deltaTime*speed, 0.0f, Space.Self);
        }
    }
}
