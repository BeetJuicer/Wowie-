using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    [SerializeField] GameObject iceCream;
    [SerializeField] GameObject cake;
    Vector3 offsetCake;
    Vector3 offsetIceCream;
    // Start is called before the first frame update
    void Start()
    {
        offsetIceCream = new Vector3(0f, 2.5f, 0f);
        offsetCake = new Vector3(0f, 2f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isCake)
        {
            transform.position = cake.transform.position + offsetCake;
        }
        else
        {
            transform.position = iceCream.transform.position + offsetIceCream;
        }
    }
}
