using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script moves the party-guests in the partyrooms a little to add some sense of life to the party

public class PartyGuestMover : MonoBehaviour
{

    private int speed;      
    public int lowerSpeedBound;     //public variables to set speed-limits (upper and lower) individually for black-metal partyguests (faster) and techno-partyguests (slower)
    public int upperSpeedBound;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(lowerSpeedBound, upperSpeedBound);     //calculate a random movement speed for each specific instance of a partyguest
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.AngleAxis((Mathf.PingPong(Time.time*speed, 20f)-10), Vector3.left);     //pingpong between to values at the speed calculated in Start()
    }
}
