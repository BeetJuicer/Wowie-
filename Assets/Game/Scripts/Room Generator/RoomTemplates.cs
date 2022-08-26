using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class RoomTemplates : MonoBehaviour
{
    public List<GameObject> topRooms = new List<GameObject> ();
    public List<GameObject> bottomRooms = new List<GameObject> ();
    public List<GameObject> rightRooms = new List<GameObject> ();
    public List<GameObject> leftRooms = new List<GameObject> ();

    public GameObject closedRoom;
    [SerializeField] private GameObject bossRoom;

    public List<GameObject> rooms;

    private void Start()
    {
        Invoke("SpawnBossRoom", 3);
    }

    private void SpawnBossRoom()
    {
        Instantiate(bossRoom, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
        Destroy(rooms[rooms.Count - 1]);
    }

}


