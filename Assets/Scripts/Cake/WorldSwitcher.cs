using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject iceCreamGroundSprite;
    [SerializeField] private GameObject cakeGroundSprite;
    [SerializeField] private GameObject cakeAdditionalSprite;
    [SerializeField] private GameObject iceCreamAdditionalSprite;

    private Tilemap cakeAdditionalTilemap;
    private Tilemap iceCreamAdditionalTilemap;
    // Start is called before the first frame update
    void Start()
    {
        cakeAdditionalTilemap = cakeAdditionalSprite.GetComponent<Tilemap>();
        iceCreamAdditionalTilemap = iceCreamAdditionalSprite.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isCake)
        {
            //Cake Values
            cakeGroundSprite.SetActive(true);
            iceCreamGroundSprite.SetActive(false);
            cakeAdditionalTilemap.color = new Color(1f, 1f, 1f, 1f);
            iceCreamAdditionalTilemap.color = new Color(1f, 1f, 1f, 0.4f);
        }
        else
        {
            //Ice Cream Values
            cakeGroundSprite.SetActive(false);
            iceCreamGroundSprite.SetActive(true);
            cakeAdditionalTilemap.color = new Color(1f, 1f, 1f, 0.4f);
            iceCreamAdditionalTilemap.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
