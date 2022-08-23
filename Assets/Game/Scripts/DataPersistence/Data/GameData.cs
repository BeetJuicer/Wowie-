using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int checkpointCount;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> coinsCollected;

    private Vector3 newGamePos = new Vector3(-171f, 2f, 0f);

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        this.checkpointCount = 0;
        playerPosition = newGamePos;//checkpoint[0]
        coinsCollected = new SerializableDictionary<string, bool>();
    }
}
