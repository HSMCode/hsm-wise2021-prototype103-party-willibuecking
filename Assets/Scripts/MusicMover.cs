using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMover : MonoBehaviour
{

    private bool isCollected = false;
    private GameObject player;
    private Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerBody");
        playerPos = GameObject.Find("PlayerBody").transform.position;
        //playerPos = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isCollected)
        {
            Debug.Log("collected");
            //transform.position = playerPos;
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //transform.position = new Vector3(0f, 0f, 0f);
            isCollected = true;
            transform.parent = player.transform;
        }
    }
}
