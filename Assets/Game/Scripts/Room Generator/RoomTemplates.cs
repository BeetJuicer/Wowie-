using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public GameObject[] rightRooms;
    public GameObject[] leftRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    private Vector3 offset = new Vector3(16, 8, 0);

    private bool spawnedBoss;
    public GameObject boss;

    private void Start()
    {
        Instantiate(boss, rooms[rooms.Count - 1].transform.position - offset, Quaternion.identity);
        spawnedBoss = true;
    }
}


