using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomLevel : MonoBehaviour
{
    public bool top, bottom;
    public bool left, right;

    public List<GameObject> doors;

    public bool startDetection;


    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("Door"))
            {
                doors.Add(child.gameObject);
            }
        }

        if (top) { doors[0].SetActive(true); }
        if (bottom) {doors[1].SetActive(true); }
        if (right) {doors[2].SetActive(true); }
        if (left) {doors[3].SetActive(true); }
        startDetection = true;
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && startDetection)
        {
            Invoke("Open", 1f);
        }
    }

    void Open()
    {
        foreach (GameObject door in doors)
        {
            if (door.activeInHierarchy)
            {
                door.GetComponent<RoomDoor>().CheckForDoor();
                door.SetActive(false);
            }
        }
    }
}
