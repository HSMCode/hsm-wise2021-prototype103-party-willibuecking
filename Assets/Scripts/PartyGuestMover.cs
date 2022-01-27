using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyGuestMover : MonoBehaviour
{

    private int speed;
    public int lowerSpeedBound;
    public int upperSpeedBound;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(lowerSpeedBound, upperSpeedBound);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.AngleAxis((Mathf.PingPong(Time.time*speed, 20f)-10), Vector3.left);
    }
}
