using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{
    private RoomTemplates templates;
    private bool spawned = false;

    private Vector3 offset = new Vector3(16, 8, 0);

    public float waitTime = 4;

    private int rand;

    public int openingDirection;
    // 1 - IS a top door, needs a bottom door
    // 2 - IS a bottom door, needs a top door
    // 3 - IS a right door, needs a left door
    // 4 - IS a left door, needs a right door

    private void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    private void Spawn()
    {
        if (spawned == false)
        {
            //-Spawn a room with a bottom door
            if (openingDirection == 1)
            {
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position - offset, Quaternion.identity);
            }
            //-Spawn a room with a top door
            else if (openingDirection == 2)
            {
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position - offset, Quaternion.identity);
            }
            //-Spawn a room with a left door
            else if (openingDirection == 3)
            {
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position - offset, Quaternion.identity);
            }
            //-Spawn a room with a right door
            else if (openingDirection == 4)
            {
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position - offset, Quaternion.identity);
            }
            spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint"))
        {
            if (collision.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedRoom, transform.position - offset, Quaternion.identity);
            }
            spawned = true;
        }
    }   
}
