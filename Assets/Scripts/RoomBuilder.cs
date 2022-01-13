using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour
{
    private Vector3[] roomOffset = new Vector3[4];
    private bool buildFlag = false;
    private Vector3 tempGO;
    public GameObject[] room;
    // Start is called before the first frame update
    
    void Start()
    {
        roomOffset[0] = new Vector3(10f, 0f, 0f);
        roomOffset[1] = new Vector3(0f, 0f, 10f);
        roomOffset[2] = new Vector3(-10f, 0f, 0f);
        roomOffset[3] = new Vector3(0f, 0f, -10f);   
    }

    // Update is called once per frame
    void Update()
    {

    }

        void OnTriggerEnter (Collider other)
    {
        if(other.tag == "Player" && !buildFlag)
        {
            Invoke("BuildRooms", 1f);
            buildFlag = true;
        }
    }
    
    void BuildRooms()
    {   
            for (int i = 0; i < roomOffset.Length; i++) 
            {
                int rnd = Random.Range(0, roomOffset.Length);
                tempGO = roomOffset[rnd];
                roomOffset[rnd] = roomOffset[i];
                roomOffset[i] = tempGO;
            }

            for(int i = 0; i < roomOffset.Length; i++)
            {
                Instantiate(room[i], transform.position + roomOffset[i], transform.rotation);
            }
        
    }


}
