using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomLevel : MonoBehaviour
{
    public bool top, bottom;
    public bool left, right;

    public List<GameObject> doors;
    public List<GameObject> enemyList;

    public bool startDetection;
    public bool startEnemyCount;

    private void Start()
    {
        startEnemyCount = false;

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
    }

    private void Update()
    {
        if (startEnemyCount)
        {
            if (enemyList.Count == 0)
            {
                Invoke("Open", 0.5f);
                startEnemyCount = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invoke("Close", 0.5f);
            foreach (GameObject enemy in enemyList)
            {
                enemy.SetActive(true);
            }
            startEnemyCount = true;
        }

        if (collision.CompareTag("Enemy"))
        {
            if (!enemyList.Contains(collision.gameObject))
            {
                enemyList.Add(collision.gameObject);
                collision.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemyList.Contains(collision.gameObject) && startEnemyCount)
        {
            enemyList.Remove(collision.gameObject);
        }
    }

    void Close()
    {
        foreach (GameObject door in doors)
            {
                if (!door.activeInHierarchy)
                {
                    door.SetActive(true);
                }
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
