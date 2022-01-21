using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    public float entranceID = 0;
    public bool entranceSet = false;

    private bool thisRoomEntered = false;

    private GameObject playerMover;
    private PlayerMoverScript playerMoverScript;

    private GameObject sceneMaster;
    private RoomMaintainer roomMaintainerScript;

    private List<Vector3> roomOffset = new List<Vector3>();
    private bool buildFlag = false;
    private Vector3 tempGO;

    private List<GameObject> buildingRooms = new List<GameObject>();

    private Vector3 pMP;
    private Vector3 rP;

    public bool isStartRoom;
    public bool isFinishRoom;

    // Start is called before the first frame update
    void Start()
    {
        playerMover = GameObject.Find("PlayerMover");
        playerMoverScript = playerMover.GetComponent<PlayerMoverScript>();
        rP = transform.position;

        sceneMaster = GameObject.Find("SceneMaster");
        roomMaintainerScript = sceneMaster.GetComponent<RoomMaintainer>();

        //-----------------------------------------------------------------------------------------------------------------------------------------------
        //this section takes care of roombuilding
        roomOffset.Add(new Vector3(-10f, 0f, 0f));
        roomOffset.Add(new Vector3(0f, 0f, 10f));
        roomOffset.Add(new Vector3(10f, 0f, 0f));
        roomOffset.Add(new Vector3(0f, 0f, -10f));
        //-----------------------------------------------------------------------------------------------------------------------------------------------
    }

    // Update is called once per frame
    void Update()
    {

        //-----------------------------------------------------------------------------------------------------------------------------------------------
        //this section takes care of roomdestruction
        pMP = playerMover.transform.position;
        if(!(((pMP.x == rP.x)&&(pMP.z >= rP.z-10 && pMP.z <= rP.z+10)) || ((pMP.z == rP.z)&&(pMP.x >= rP.x-10 && pMP.x <= rP.x+10))) )
        {
            if(!playerMoverScript.isChangingRooms)
            {
                DestroyRoomInit();
            }   
        }

        if(roomMaintainerScript.finished && !isFinishRoom)
        {
            DestroyRoomInit();
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !thisRoomEntered)
        {
            thisRoomEntered = true;
        }

        if(other.tag == "Player" && !buildFlag)
        {
            

            if(isStartRoom)
            {
                buildingRooms.Add(roomMaintainerScript.partyRooms[0]);
                buildingRooms.Add(roomMaintainerScript.partyRooms[1]);
                buildingRooms.Add(roomMaintainerScript.musicRoomA);
                buildingRooms.Add(roomMaintainerScript.musicRoomT);
            }
            else
            {
                roomOffset.RemoveAt((int) entranceID);

                if(roomMaintainerScript.aPartyBuildFlag)
                {
                    buildingRooms.Add(roomMaintainerScript.partyRooms[0]);
                    roomMaintainerScript.aPartyBuildFlag = false;
                }
                else
                {
                    buildingRooms.Add(roomMaintainerScript.partyRooms[1]);
                    roomMaintainerScript.aPartyBuildFlag = true;
                }
                buildingRooms.Add(roomMaintainerScript.musicRoomA);
                buildingRooms.Add(roomMaintainerScript.musicRoomT);
            }

            Invoke("BuildRooms", 0f);
            buildFlag = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            buildFlag = false;

            roomOffset.Clear();

            roomOffset.Add(new Vector3(-10f, 0f, 0f));
            roomOffset.Add(new Vector3(0f, 0f, 10f));
            roomOffset.Add(new Vector3(10f, 0f, 0f));
            roomOffset.Add(new Vector3(0f, 0f, -10f));

            entranceSet = false;

            roomMaintainerScript.roomCount ++;

        }
    }

    void BuildRooms()
    {
        for (int i = 0; i < roomOffset.Count; i++) 
            {
                int rnd = Random.Range(0, roomOffset.Count);
                tempGO = roomOffset[rnd];
                roomOffset[rnd] = roomOffset[i];
                roomOffset[i] = tempGO;
            }

            for(int i = 0; i < roomOffset.Count; i++)
            {
                Instantiate(buildingRooms[i], transform.position + roomOffset[i], transform.rotation);
            }
    }

    void DestroyRoomInit()
    {
        StartCoroutine("DestroyRoom");
        transform.Translate(Vector3.down * Time.deltaTime*20, Space.World);
    }

    IEnumerator DestroyRoom()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
