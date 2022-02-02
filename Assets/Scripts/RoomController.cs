using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to every room and takes care of its own destruction and the building of other rooms 

public class RoomController : MonoBehaviour
{

    public float entranceID = 0;        //this is set by LightSwitch.cs upon room-entrance to hold the number of the door from which the Player entered the room
                                        //it is later used in roombuilding to leave out the side from which the player entered
    public bool entranceSet = false;    //set to "true" once the player enters thus setting an entranceID and keeping it from beeing changed while moving through the room later on

    private bool thisRoomEntered = false;   //used to check if this room is eligible for destrucion

    private GameObject playerMover;     
    private PlayerMoverScript playerMoverScript;        //get some info from the Player and the attached scripts

    private GameObject sceneMaster;
    private RoomMaintainer roomMaintainerScript;        //get variables from the SceneMaster and its corresponding script RoomMaintainer.cs

    private List<Vector3> roomOffset = new List<Vector3>(); //make a list for all possible places for new rooms to be built at
    private bool buildFlag = false;     //flag for checking if new rooms have already been built
    private Vector3 tempGO;     //used in roombuilding to hold a value inside a for-loop

    private List<GameObject> buildingRooms = new List<GameObject>(); //make a list for the rooms that have to be built

    private Vector3 pMP;     //the position of the PlayerMover (set later on), used for keeping things readable by using a short variable name
    private Vector3 rP;     //the absolute position of this room in world space (set later on)

    public bool isStartRoom;    //flags for checking if this room is the starting or the finishing room
    public bool isFinishRoom;

    // Start is called before the first frame update
    void Start()
    {
        playerMover = GameObject.Find("PlayerMover");
        playerMoverScript = playerMover.GetComponent<PlayerMoverScript>();      //get The PlayerMoverScript.cs script
        rP = transform.position;        //set this rooms absoulte position into a small variable for readybility later

        sceneMaster = GameObject.Find("SceneMaster");
        roomMaintainerScript = sceneMaster.GetComponent<RoomMaintainer>();      //get the RoomMaintainer.cs script from the SceneMaster object

        roomOffset.Add(new Vector3(-10f, 0f, 0f));      //write the four possible positions for newly built adjacent rooms to be built at into the roomOffset List
        roomOffset.Add(new Vector3(0f, 0f, 10f));
        roomOffset.Add(new Vector3(10f, 0f, 0f));
        roomOffset.Add(new Vector3(0f, 0f, -10f));
    }

    // Update is called once per frame
    void Update()
    {

        //-----------------------------------------------------------------------------------------------------------------------------------------------
        //this section takes care of roomdestruction
        //rooms get destroyed if the player is not in a directly adjacent one. Only the rooms which can currently be entered by one single button press are kept alive
        pMP = playerMover.transform.position;       //set PlayerMovers transform position into this small variable so the condition for roomdestruction is readable
        if(!(((pMP.x == rP.x)&&(pMP.z >= rP.z-10 && pMP.z <= rP.z+10)) || ((pMP.z == rP.z)&&(pMP.x >= rP.x-10 && pMP.x <= rP.x+10))) )      //check if the PlayerMover (the center that the PlayerBody rotates around) is in one of the rooms adjacent to this one
                                                                                                                                            //this is done by checking if the PlayerMover is on one of the axis´ of this room while being not further away than 10 Measurements on the other axis
        {
            if(!playerMoverScript.isChangingRooms)      //if the player is not one of the directly adjacent rooms and not currently in the process of moving between rooms, this room can be destroyed which is done by calling a Destroy-function (see below)
            {
                DestroyRoomInit();
            }   
        }

        if(roomMaintainerScript.finished && !isFinishRoom)      //if the player enters a partyroom, thus finishing the game, all rooms except the finishing room are destroyed
        {                                                       //this is done so potential musical content of adjacent room doesn´t spill into the party and ruin the track the player has composed
            DestroyRoomInit();
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !thisRoomEntered)       //if the player enters this room for the first time, the tracksLeft-variable of RoomMaintainer.cs is lowered by one and this room is set as having been entered
        {
            roomMaintainerScript.tracksLeft -= 1;       
            thisRoomEntered = true;                     //this flag is used to keep the script from reducing the value of roomMaintainerScript.tracksLeft at every frame update
        }

        if(other.tag == "Player" && !buildFlag)     //if the Player enters this room and buildFlag is still false (meaning no rooms have been built by this one yet), the building process is started
        {                                           //the first step is to fill the buildingRooms list with the rooms that should be built
            

            if(isStartRoom)     //if this room is the starting-room, four rooms get added to the list of rooms to build: both party-rooms and both music-rooms
            {
                buildingRooms.Add(roomMaintainerScript.partyRooms[0]);
                buildingRooms.Add(roomMaintainerScript.partyRooms[1]);
                buildingRooms.Add(roomMaintainerScript.musicRoomA);
                buildingRooms.Add(roomMaintainerScript.musicRoomT);
            }
            else
            {
                roomOffset.RemoveAt((int) entranceID);      //if this is not the starting room, only three rooms get built, two music-rooms and a party room.
                                                            //the roomOffset that correspond to the entrance used by the player gets removed from the list of possible roomoffsets
                                                            //this means, that at the position where the previously visited room is, no new room gets built as the old one doesn´t get destroyed
                                                            //this is because the room where the player came from is kept alive because destroying it felt weird and it gives the possibility to enter a room that doesn nothing
                                                            //having this option is important if the player sees none of the tracks to be fit for his mix or if he has collected all the music he wants/needs but the existing party-room is of the wrong genre
                                                            //players can then move back to the emptyroom they came from to create new rooms and especially a different party
                if(roomMaintainerScript.aPartyBuildFlag)        //if the black-metal party buildflag is true, a black-metal partyroom gets added to the list of rooms to build and the flag is set false
                {
                    buildingRooms.Add(roomMaintainerScript.partyRooms[0]);
                    roomMaintainerScript.aPartyBuildFlag = false;
                }
                else                                            //if the black-metal party buildflag is false, a techno partyroom gets added to the building-list and the flag is set true
                {                                               //this ensures, that the two party-rooms are always alternating between moves
                    buildingRooms.Add(roomMaintainerScript.partyRooms[1]);
                    roomMaintainerScript.aPartyBuildFlag = true;
                }
                buildingRooms.Add(roomMaintainerScript.musicRoomA); //in the end, add a black-metal MusicRoom and a Techno MusicRoom to the list
                buildingRooms.Add(roomMaintainerScript.musicRoomT);
            }

            Invoke("BuildRooms", 0.3f);     //now that the script picked the rooms designated for building, it calls the BuildRooms function with a short delay of 0.3 seconds
                                            //the functino takes care of the actual buildingprocess and the delay is so the old roomms can be destroyed before the new ones are being built
            buildFlag = true;               //if the buildingRooms list is filled and the building-function has been called, the buildFlag is set tot true, so the process can´t repeat
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

        }
    }

    void BuildRooms()       //this is the function actually building the rooms
    {
        for (int i = 0; i < roomOffset.Count; i++)      //it takes the list of possible room positions (above, left, right und below this room minus the one where the previous room is)
            {                                           //and randomly reorders it to ensure that the roomlayout (position of partyroom and musicroom relativ to this room) aren´t always the same
                int rnd = Random.Range(0, roomOffset.Count);
                tempGO = roomOffset[rnd];
                roomOffset[rnd] = roomOffset[i];
                roomOffset[i] = tempGO;
            }

            for(int i = 0; i < roomOffset.Count; i++)       //in a second loop the function iterates over the list of all possible relative room-positions in its new random order
            {
                Instantiate(buildingRooms[i], transform.position + roomOffset[i], transform.rotation); //and builds a room from the list with the rooms designated for building at these positions
            }
    }

    void DestroyRoomInit()      //this function takes care of room-destruction
    {
        StartCoroutine("DestroyRoom");      //it starts a coroutine that actually destroys the room (after a short waiting-time)
        transform.Translate(Vector3.down * Time.deltaTime*20, Space.World);     //and then starts rapidly moving the room away from the camera
                                                                                //this is done for two reasons:
                                                                                //1. it looks pretty cool
                                                                                //2. so the musicTrack contained in the room exits the rooms collider as opposed to the collider just disappearing
                                                                                //this is used in MusicMover.cs to move the Track back to its original position out of listening range so it doesnß t keep on playing near the player and disrupting the game
    }

    IEnumerator DestroyRoom()       //this actually destroys the room
    {
        yield return new WaitForSeconds(0.5f);      //wait for half a second (so the room has time to move away and do the whole trigger-exit thing and all)
        Destroy(gameObject);                        //destroy this room
    }
}
