using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    public float entranceID;
    public bool entranceSet = false;

    private bool thisRoomEntered = false;

    private GameObject playerMover;
    private PlayerMoverScript playerMoverScript;

    private Vector3 pMP;
    private Vector3 rP;

    // Start is called before the first frame update
    void Start()
    {
        playerMover = GameObject.Find("PlayerMover");
        playerMoverScript = playerMover.GetComponent<PlayerMoverScript>();

        rP = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pMP = playerMover.transform.position;
        if(!(thisRoomEntered || ((pMP.x == rP.x)&&(pMP.z >= rP.z-10 && pMP.z <= rP.z+10)) || ((pMP.z == rP.z)&&(pMP.x >= rP.x-10 && pMP.x <= rP.x+10))) )
        {
         DestroyRoomInit();   
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !thisRoomEntered)
        {
            thisRoomEntered = true;
        }
    }

    void DestroyRoomInit()
    {
        StartCoroutine("DestroyRoom");
    }

    IEnumerator DestroyRoom()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
