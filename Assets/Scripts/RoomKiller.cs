using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomKiller : MonoBehaviour
{
    public GameObject player;
    private float dist;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(player.transform.position, transform.position);
        //Debug.Log(dist);
        if(dist > 15)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit (Collider other)
    {
        Destroy(gameObject);
    }
}
