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

    public float waitTime = 2;

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
                SpawnRoom(templates.bottomRooms);
            }
            //-Spawn a room with a top door
            else if (openingDirection == 2)
            {
                SpawnRoom(templates.topRooms);
            }
            //-Spawn a room with a left door
            else if (openingDirection == 3)
            {
                SpawnRoom(templates.leftRooms);
            }
            //-Spawn a room with a right door
            else if (openingDirection == 4)
            {
                SpawnRoom(templates.rightRooms);
            }
            spawned = true;
        }
    }

    private void SpawnRoom(List<GameObject> roomList)
    {
        rand = Random.Range(0, roomList.Count);
        Instantiate(roomList[rand], transform.position - offset, Quaternion.identity);
        RemoveRoomFromLists(roomList[rand]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint"))
        {
            if (collision.GetComponent<RoomSpawner>().spawned == false && spawned == false && transform.position.x != 16 && transform.position.y != 8)
            {
                Instantiate(templates.closedRoom, transform.position - offset, Quaternion.identity);
            }
            spawned = true;
        }
    }
    
    private void RemoveRoomFromLists(GameObject room)
    {
        if (templates.topRooms.Contains(room))
        {
            templates.topRooms.Remove(room);
        }
        if (templates.bottomRooms.Contains(room))
        {
            templates.bottomRooms.Remove(room);
        }
        if (templates.leftRooms.Contains(room))
        {
            templates.leftRooms.Remove(room);
        }
        if (templates.rightRooms.Contains(room))
        {
            templates.rightRooms.Remove(room);
        }
    }
}
